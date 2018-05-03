module lib

open canopy
open canopy.configuration
open etconfig
open locators


let login (userId : string) (password: string) =
    try
        displayed signin_txt
        loginEmail_textbox << userId
        click next_button
        loginPassword_textbox << password
        click next_button
    with
    | _ ->
        reporter.write "Email Login Failed"

let emailLogout () =
    try
        click (xpath virtruUserid_button)
        click virtruLogout_dditem_logout
        switchToTab 1
        click (xpath gmailUserId_button)
        click gmailSignOut_button

    with
    | _ ->
        reporter.write "Logout/s failed"

//let emailLogin () = login config.userId config.password
