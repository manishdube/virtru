module Virtru_test_feature

open canopy
open etconfig
open locators
open lib
open secret

let all _ = 
    context "Virtru_test_feature"

    //once (fun _ -> emailLogin ())
    lastly (fun _ -> emailLogout ())

    "Given I log in to gmail.com with username 'xxx'and password 'xxx' "&&& fun _ ->
        url (uri) 
        displayed signin_txt
        loginEmail_textbox << userId
        click next_button
        loginPassword_textbox << password
        click next_button

    "When I search for an expected email i am able to open that email" &&& fun _ ->
        gmailSearch_textbox << searchterm
        press enter
        click email

    "Then I verify the expected contents in the email" &&& fun _ ->
        click (xpath secureMessage)
        switchToTab 2
        click userId
        click loginWithGoogle
        contains secureMessageContent (read secureMessageBody)

