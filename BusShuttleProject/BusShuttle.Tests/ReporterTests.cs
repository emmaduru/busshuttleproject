namespace BusShuttle.Tests;

using BusShuttle;

public class ReporterTests {
    List<PassengerData> sampleDataList;

    public ReporterTests() {
        sampleDataList = new List<PassengerData>();
    }

    [Fact]
    public void Test_FindBusiestStop_Just2Stops() {
        Stop sampleStop = new Stop("MyStop");
        Loop sampleLoop = new Loop("MyLoop");
        Driver sampleDriver = new Driver("Sample");
        PassengerData sampleData = new PassengerData(5, sampleStop, sampleLoop, sampleDriver);
        sampleDataList.Add(sampleData);
        
        Stop sampleStop2 = new Stop("MyStop2");
        PassengerData sampleData2 = new PassengerData(6, sampleStop2, sampleLoop, sampleDriver);
        sampleDataList.Add(sampleData2);

        var result = Reporter.FindBusiestStop(sampleDataList);
        Assert.Equal("MyStop2", result.Name);
    }

   [Fact]
    public void Test_FindBusiestStop_Just2Stops_MoreData() {
        sampleDataList.Add(new PassengerData(4, new Stop("MyStop"), new Loop("MyLoop"), new Driver("Sample")));
        sampleDataList.Add(new PassengerData(5, new Stop("MyStop2"), new Loop("MyLoop"), new Driver("Sample")));
        sampleDataList.Add(new PassengerData(2, new Stop("MyStop"), new Loop("MyLoop"), new Driver("Sample")));
        

        var result = Reporter.FindBusiestStop(sampleDataList);
        Assert.Equal("MyStop", result.Name);
    }

}