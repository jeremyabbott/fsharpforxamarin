module LayoutHelpers

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