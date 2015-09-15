module LayoutHelpers

open System
open Foundation
open UIKit
open Praeclarum.AutoLayout

let addConstraints (view : UIView) constraints =
    view.AddConstraints constraints
    Array.iter (fun (v : UIView) -> v.TranslatesAutoresizingMaskIntoConstraints <- false) view.Subviews

let centerViewInParent (view : UIView) (parent : UIView) =
    addConstraints parent [|view.LayoutCenterX == parent.LayoutCenterX
                            view.LayoutCenterY == parent.LayoutCenterY|]

// source; https://github.com/dvdsgl/shallow/blob/master/Shallow/UIKitExtensions.fs
type UIImage with
    static member FromUrl(url) =
        url
        |> NSUrl.FromString
        |> NSData.FromUrl
        |> UIImage.LoadFromData

type LoadingLabel (text) as x =
    inherit UIView()
    let loadingLabel = new UILabel(Text = text, TextAlignment = UITextAlignment.Center, TextColor = UIColor.White)
    let activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge)
    do
        x.BackgroundColor <- UIColor.Black
        x.Alpha  <- (nfloat 0.75)
        x.AddSubviews (loadingLabel, activitySpinner)
        addConstraints x [|activitySpinner.LayoutCenterX == x.LayoutCenterX
                           activitySpinner.LayoutCenterY == x.LayoutCenterY
                           activitySpinner.LayoutWidth == x.LayoutWidth * nfloat 0.5
                           activitySpinner.LayoutHeight == activitySpinner.LayoutHeight
                           loadingLabel.LayoutBottom == activitySpinner.LayoutTop - nfloat 10.
                           loadingLabel.LayoutCenterX == activitySpinner.LayoutCenterX|]
    member x.Hide(complete) =
        UIView.Animate(5.0, (fun _ ->
                                x.Alpha <- nfloat 0.
                                x.RemoveFromSuperview()
                                complete()))
