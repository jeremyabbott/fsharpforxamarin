namespace fsharpios

open UIKit
open GitHub.Data
open LayoutHelpers
open Praeclarum.AutoLayout

type GistsTableViewController() = 
    inherit UIViewController()

    let loadingIndicator = new LoadingLabel("Loading...")
    let gistTable = new UITableView()
    let getContent statusBarHeight =
        let view = new UIView(BackgroundColor = UIColor.White)

        view.AddSubview gistTable
        view.AddSubview loadingIndicator

        addConstraints view [|gistTable.LayoutTop == view.LayoutTop + statusBarHeight
                              gistTable.LayoutWidth == view.LayoutWidth
                              gistTable.LayoutBottom == view.LayoutBottom

                              loadingIndicator.LayoutCenterX == view.LayoutCenterX
                              loadingIndicator.LayoutCenterY == view.LayoutCenterY
                              loadingIndicator.LayoutTop == view.LayoutTop
                              loadingIndicator.LayoutTop == view.LayoutBottom
                              loadingIndicator.LayoutWidth == view.LayoutWidth|]
        view

    let loadData viewController = 
        async {
            let! gists = getGistsAsync()
            gistTable.Source <- new GistDataSource(getGists(), viewController)
            gistTable.ReloadData()
            loadingIndicator.Hide((fun _ -> gistTable.Hidden <- false))
        }
        
    override x.ViewDidLoad () =
        base.ViewDidLoad()
        gistTable.Hidden <- true
        x.View <- getContent UIApplication.SharedApplication.StatusBarFrame.Height

    override x.ViewWillAppear animated =
        base.ViewWillAppear animated
        Async.StartImmediate(loadData x)