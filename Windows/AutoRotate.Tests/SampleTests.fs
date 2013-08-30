namespace AutoRotate.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open MultiMonitorHelper
open MultiMonitorHelper.Common.Enum
open MultiMonitorHelper.Common.Interfaces

[<TestClass>]
type SampleTests() = 
    
    [<TestMethod>]
    member this.``Test the test configuration``() = 
        Assert.IsTrue(true)


    [<TestMethod>]
    member this.``Find the number of displays``() = 
        let myNumberOfDisplays = 2
        let displayModel = MultiMonitorHelper.DisplayFactory.GetDisplayModel()
        Assert.AreEqual(myNumberOfDisplays, displayModel.GetActiveMonitors() |> Seq.length)
