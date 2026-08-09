
// namespace Kafka.Operations.Inbox;

// partial class InboxTests
// {
//   [TestMethod]
//   public void CaptureKafkaMessage_ShouldReturnCapturedState_WhenMessageIsValid()
//   {
//     // Arrange
//     var services = new Mock<ICaptureKafkaMessageServices<string, string>>();
//     var data = new Mock<ICaptureKafkaMessageData<string, string, string>>();
//     var message = new Message<string, string> { Key = "key", Value = "value" };
//     var topicPartitionOffset = new TopicPartitionOffset("topic", 0, 0);
//     var consumeResult = new ConsumeResult<string, string>
//     {
//       Message = message,
//       TopicPartitionOffset = topicPartitionOffset
//     };

//     services.Setup(s => s.GetConsumer()).Returns(new Mock<IConsumer<string, string>>().Object);
//     services.Setup(s => s.GetConsumer().Consume(It.IsAny<CancellationToken>())).Returns(consumeResult);

//     // Act
//     var result = InboxFuncs.CaptureKafkaMessage(services.Object, data.Object);

//     // Assert
//     Assert.AreEqual(CapturedKafkaMessageState, result.Result.Item2);
//     Assert.AreEqual(message, result.Result.Item1.KafkaMessage);
//     Assert.AreEqual(topicPartitionOffset, result.Result.Item1.TopicPartitionOffset);
//   }

//   class CaptureKafkaMessageServices<TKey, TValue> : InstrumentationServices, ICaptureKafkaMessageServices<TKey, TValue>
//   {
//     public IConsumer<TKey, TValue> GetConsumer() => throw new NotImplementedException();
//   }
// }