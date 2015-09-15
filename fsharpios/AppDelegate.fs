namespace fsharpios

open System
open UIKit
open Foundation

type RootViewController(title) =
    inherit UIViewController()
    let content =
        let view = new UIView(BackgroundColor = UIColor.White)
        view

    override x.ViewDidLoad() =
        x.View <- content
        x.Title <- title

[<Register("AppDelegate")>]
type AppDelegate() = 
    inherit UIApplicationDelegate()
    override val Window = null with get, set

    override this.FinishedLaunching(app, options) = 
        let root1 = new RootViewController("F# is Awesome")

        let navController =
            let title = "C# is Okay Too"
            let root2 = new RootViewController(title)
            new UINavigationController(root2, Title = title)

        let tabBarController = 
            let tvc = new UITabBarController()
            tvc.ViewControllers <- [|root1; navController|]
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