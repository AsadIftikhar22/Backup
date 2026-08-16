namespace Salam.Cms.UnitTests.Features.Pages.NotFound;

using EPiServer.Core;

public static class NotFoundHeroBlockViewComponentTestCases
{
    public static IEnumerable<TestCaseData> ImageFallbackTests
    {
        get
        {
            var blockImageReference = new ContentReference(123);
            var fallbackImageReference = new ContentReference(456);

            yield return new TestCaseData(blockImageReference, null, blockImageReference);
            yield return new TestCaseData(blockImageReference, ContentReference.EmptyReference, blockImageReference);
            yield return new TestCaseData(blockImageReference, fallbackImageReference, blockImageReference);
            yield return new TestCaseData(ContentReference.EmptyReference, fallbackImageReference, fallbackImageReference);
            yield return new TestCaseData(null, fallbackImageReference, fallbackImageReference);
        }
    }
}