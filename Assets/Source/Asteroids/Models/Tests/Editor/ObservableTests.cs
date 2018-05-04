using NUnit.Framework;

public class ObservableTests
{
    [Test]
    public void OnUpdatedTest()
    {
        int newValue = 10;
        var observable = new Observable<int>();
        observable.OnUpdated += value => Assert.That(value, Is.EqualTo(newValue));

        observable.Value = newValue;

        Assert.That(observable.Value, Is.EqualTo(newValue));
    }
}