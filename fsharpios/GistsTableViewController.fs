namespace fsharpios

open UIKit
open GitHub.Data
open LayoutHelpers
open Praeclarum.AutoLayout

type GistsTableViewController() = 
    inherit UIViewController()

    let gistTable = new UITableView()
    let getContent statusBarHeight =
        let view = new UIView()

        view.AddSubview gistTable

        addConstraints view [|gistTable.LayoutTop == view.LayoutTop + statusBarHeight
                              gistTable.LayoutWidth == view.LayoutWidth
                              gistTable.LayoutBottom == view.LayoutBottom|]
        view

    override x.ViewDidLoad () =
        base.ViewDidLoad()
        x.View <- getContent UIApplication.SharedApplication.StatusBarFrame.Height

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated
        gistTable.Source <- new GistDataSource(getGists(), x)
        gistTable.ReloadData()