using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class VideoServiceTests
    {
        private Mock<IFileReader> _fileReader;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {

            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WhenCalled_ReturnUnprocessedVideoAsVsc()
        {
            var videoRepository = new Mock<IVideoRepository>();
            var videos = new Video[] {
                new Video() {Id=1, Title="a", IsProcessed=false},
                new Video() {Id=2, Title="b", IsProcessed=false}
            };
            videoRepository.Setup(vr => vr.GetVideos()).Returns(videos);

            var VideoService = new VideoService(null, videoRepository.Object);

            var result = VideoService.GetUnprocessedVideosAsCsv();
            
            Assert.That(result, Does.Contain("1"));
            Assert.That(result, Does.Contain("2"));
        }
    
    }
}