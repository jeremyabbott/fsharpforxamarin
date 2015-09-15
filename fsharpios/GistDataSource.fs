namespace fsharpios

open System
open GitHub.Data
open UIKit
open LayoutHelpers

type GistDataSource (source: Gist array, parent: UIViewController) =
    inherit UITableViewSource()

    let cellIdentifier = "GistCell"

    override x.GetCell(view, indexPath) : UITableViewCell =
        let gist = source.[int indexPath.Row]

        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier)
            | cell -> cell
        

        if gist.Owner.IsSome then cell.ImageView.Image <- (UIImage.FromUrl gist.Owner.Value.AvatarUrl)
        cell.TextLabel.Text <- gist.Id
        cell.TintColor <- UIColor.White
        cell.Accessory <- UITableViewCellAccessory.DisclosureIndicator

        cell

    override x.RowsInSection(view, section) = nint source.Length

    override x.RowSelected (tableView, indexPath) = 
        //parent.NavigationController.PushViewController(new FilmDetailViewController(filmSource.[int indexPath.Item]), true)
        tableView.DeselectRow (indexPath, false)