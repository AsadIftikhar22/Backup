namespace Salam.Cms.Shared.Models.Validation;

using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.DataAnnotations;
using EPiServer.Find.Helpers;
using EPiServer.ServiceLocation;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class LimitedBlocksInRichTextAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "The '{0}' property is only allowed to contain the following block types: {1}.";

    private readonly List<Type> _allowedTypes;

    public LimitedBlocksInRichTextAttribute(params Type[]? allowedTypes) : base(DefaultErrorMessage)
    {
        _allowedTypes = allowedTypes?.ToList() ?? new List<Type>(0);
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return value switch
        {
            XhtmlString richText => IsValidRichText(richText),
            _ => true
        };
    }

    public override string FormatErrorMessage(string name)
    {
        var blockNames = string.Join(", ", GetTypeDisplayNames());

        return string.Format(DefaultErrorMessage, name, blockNames);
    }

    private IEnumerable<string> GetTypeDisplayNames()
    {
        foreach (var type in _allowedTypes)
        {
            var displayName = type.CustomAttributes
                                  .Where(x => x.AttributeType == typeof(ContentTypeAttribute))
                                  .SelectMany(x => x.NamedArguments)
                                  .Where(x => x.MemberName.Equals("DisplayName"))
                                  .Select(x => x.TypedValue.Value?.ToString())
                                  .FirstOrDefault();

            yield return displayName ?? type.Name;
        }
    }

    private bool IsValidRichText(XhtmlString? richText)
    {
        if (richText?.Fragments == null || richText.IsEmpty)
        {
            return true;
        }

        var contentFragments = richText.Fragments
                                       .Where(x => x is ContentFragment)
                                       .Cast<ContentFragment>()
                                       .ToList();

        contentFragments.AddRange(richText.Fragments
                                          .Where(x => x is PersonalizedContentFragment)
                                          .Cast<PersonalizedContentFragment>()
                                          .SelectMany(x => x.Fragments)
                                          .Where(x => x is ContentFragment)
                                          .Cast<ContentFragment>()
                                          .ToList());

        if (!contentFragments.Any())
        {
            return true;
        }

        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

        foreach (var contentFragment in contentFragments)
        {
            if (contentLoader.TryGet<BlockData>(contentFragment.ContentLink, out var blockData))
            {
                var isAllowed = _allowedTypes.Any(x => blockData.GetObjectTypes().Any(y => y == x));

                if (!isAllowed)
                {
                    return false;
                }
            }
        }

        return true;
    }
}