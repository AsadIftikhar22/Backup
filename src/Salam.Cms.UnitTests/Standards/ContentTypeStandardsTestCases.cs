namespace Salam.Cms.UnitTests.Standards;

using EPiServer.Core;
using Salam.Cms.Web.Features.Home.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ContentTypeStandardsTestCases
{
    public static IEnumerable<TestCaseData> PageDataPropertyTestCases
    {
        get
        {
            return GetContentPropertyTestCases(typeof(PageData));
        }
    }

    public static IEnumerable<TestCaseData> BlockDataPropertyTestCases
    {
        get
        {
            return GetContentPropertyTestCases(typeof(BlockData));
        }
    }

    public static IEnumerable<TestCaseData> GetContentPropertyTestCases(Type contentBaseType)
    {
        var assembly = Assembly.GetAssembly(typeof(HomePage));

        if (assembly == null)
        {
            yield break;
        }

        var contentTypes = assembly.GetTypes()
                                   .Where(t => t.IsSubclassOf(contentBaseType))
                                   .ToList();

        foreach (var contentType in contentTypes)
        {
            var properties = contentType.GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.DeclaringType == contentType)
                {
                    yield return new TestCaseData(contentType, propertyInfo);
                }
            }
        }
    }
}
