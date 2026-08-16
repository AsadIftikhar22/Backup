namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Advanced.CMS.AdvancedReviews;
using EPiServer.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class AdvancedReviewServiceExtensions
{
    public static IServiceCollection AddCmsAdvancedReviews(this IServiceCollection services)
    {
        return services.AddAdvancedReviews(options =>
        {
            options.IsEnabled = true;
            options.IsReviewCommentsCommandEnabled = true;
            options.IsAdminModePinReviewerPluginEnabled = true;

            // No point in enabling editable links as this requires CMS Access
            options.EditableLinksEnabled = false;

            options.EmailSubject = "Salam Website Content Review";
            options.EmailEdit = GetEmailEditorBody();
            options.EmailView = GetEmailReviewerBody();

            options.PinCodeSecurity.Enabled = true;
            options.PinCodeSecurity.Required = true;
            options.PinCodeSecurity.CodeLength = 8;
            options.PinCodeSecurity.AuthenticationCookieLifeTime = TimeSpan.FromMinutes(15);
            options.PinCodeSecurity.RolesWithoutPin = new[]
            {
                Roles.CmsAdmins,
                "SecurityAdmins"
            };
        });
    }

    private static string? GetEmailEditorBody()
    {
        return GetEmailBody("Salam.Web.Infrastructure.AdvancedReviews.EmailEditorBody.txt");
    }

    private static string? GetEmailReviewerBody()
    {
        return GetEmailBody("Salam.Web.Infrastructure.AdvancedReviews.EmailReviewerBody.txt");
    }

    private static string? GetEmailBody(string resourceName)
    {
        try
        {
            var assembly = Assembly.GetAssembly(typeof(AdvancedReviewServiceExtensions));
            using var stream = assembly!.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);

            return reader.ReadToEnd();
        }
        catch (Exception)
        {
            return null;
        }
    }
}