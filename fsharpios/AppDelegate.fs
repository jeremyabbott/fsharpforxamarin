namespace fsharpios

open System
open UIKit
open Foundation
open LayoutHelpers

type RootViewController(title, labelText) =
    inherit UIViewController()
    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        let label = new UILabel(Text = labelText)
        view.AddSubview label
        centerViewInParent label view
        view

    override x.ViewDidLoad() =
        x.View <- content
        x.Title <- title

[<Register("AppDelegate")>]
type AppDelegate() = 
    inherit UIApplicationDelegate()
    override val Window = null with get, set

    override this.FinishedLaunching(app, options) = 
        let root1 = new RootViewController("F# is Awesome", "Label #1")

        let navController =
            let title = "C# is Okay Too"
            let root2 = new RootViewController(title, "Label #2")
            new UINavigationController(root2, Title = title)
        
        let gists = new GistsTableViewController(Title = "Gists via Type Provider")

        let tabBarController = 
            let tvc = new UITabBarController()
            tvc.ViewControllers <- [|root1; navController; gists|]
            tvc

        this.Window <- new UIWindow(UIScreen.MainScreen.Bounds)
        this.Window.RootViewController <- tabBarController
        this.Window.MakeKeyAndVisible()
        true

module Main = 
    [<EntryPoint>]
    let main args = 
        UIApplication.Main(args, null, "AppDelegate")
        0