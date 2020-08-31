using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _bookingRepository;
        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _existingBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = new DateTime(2020, 9, 20),
                DepartureDate = new DateTime(2020, 9, 27),
                Reference = "a"
            };

            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking> { _existingBooking }.AsQueryable());

        }

        [Test]
        public void ExistingBookingIsCancelled_ReturnEmptyString()
        {
            var booking = new Booking() { Status = "Cancelled" };
            var result = BookingHelper.OverlappingBookingsExist(booking);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartAndFinishBeforeAnExistingBooking_ReturnEmptyString()
        {
            var booking = new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartAfterAnExistingBookingFinished_ReturnEmptyString()
        {

            var booking = new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate, 2),
                DepartureDate = After(_existingBooking.DepartureDate, 3)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartBeforeAnExistingBookingFinish_ReturnEmptyString()
        {

            var booking = new Booking() 
            { 
                Id = 1, 
                ArrivalDate = After(_existingBooking.ArrivalDate, 2), 
                DepartureDate = Before(_existingBooking.DepartureDate, 1)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartBeforeAnExistingBookingStartAndFinishAfterExistingBookingFinish_ReturnEmptyString()
        {

            var booking = new Booking() 
            { 
                Id = 1, 
                ArrivalDate = Before(_existingBooking.ArrivalDate), 
                DepartureDate = After(_existingBooking.DepartureDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
    }

}
