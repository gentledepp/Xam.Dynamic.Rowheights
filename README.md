# Xam.Dynamic.Rowheights
Bug repro for Xamarin.Forms iOS ListViewRenderer when having different custom ViewCells with varying heights



This bug is known since Xamarin.Forms 2.3.x but we hoped that it would be fixed in one of the upcoming prereleases, but in 2.3.4.214-pre5 the issue still exists.

I created a public github repo (which has Xamarin.Forms as submodule as I could not have debugged it otherwise)
https://github.com/gentledepp/Xam.Dynamic.Rowheights


When you run it on Android or UWP, you get a list, where the second item ("Checkbox") is considerably higher and shows the subviews "a", "b" and "c"

On iOS, however, this view is cut off and you cannot se "a", "b" or "c"
