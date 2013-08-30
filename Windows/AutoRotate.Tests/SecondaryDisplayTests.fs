namespace AutoRotate.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open MultiMonitorHelper
open MultiMonitorHelper.Common.Enum
open MultiMonitorHelper.Common.Interfaces

[<TestClass>]
type SecondaryDisplayTests() = 
    
    [<TestMethod>]
    member this.``Ensure there is only one primary display``() =
        let displayModel = MultiMonitorHelper.DisplayFactory.GetDisplayModel()
        let primaries = 
            displayModel.GetActiveMonitors() 
            |> Seq.filter (fun display -> display.Settings.IsPrimary) 
            |> Seq.length

        Assert.AreEqual(1, primaries)

    [<TestMethod>]
    [<Ignore>]
    (* The following test is actually a failure when the following conditions are met : 
    - The primary display is the "second" display (e.g. with a name like \\\DISPLAY.2) --> its "origin" will have an xOffset of e.g. 1920
    - This was set as the primary display in the "screen resolution" panel.
    ==> the IsPrimary method is actually broken.
    *)
    member this.``Rotate secondary Display``() = 

        let secondaryDisplay = 
            MultiMonitorHelper.DisplayFactory.GetDisplayModel().GetActiveMonitors()
            |> Seq.filter (fun display -> not display.Settings.IsPrimary)
            |> fun ds -> match ds with
                         | ds when Seq.isEmpty ds -> None
                         | ds -> Some(Seq.head ds)

        match secondaryDisplay with
        | Some display -> do display.Rotate(DisplayRotation.Rotated90)
        | _ -> ()

        Assert.Fail()