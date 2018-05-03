Given I opened the main page
When I trigger the FEPOC Notification as "ORDER_SHPD_CONS" "R60655075" "13871689" "NUM_OF_RX=3" "CARRIER_NAME= USPS" "SHIP_DT= mm/dd/2018" "TRACKING_NO=1234" "SIGN_REQ_IND=True
And The "MyBlue" page is successfully loaded
Given I input a username "R2UAT510"
Given I input a password "Uattest@1"
When I click submit button
When I click the link for a different verification way
And I choose use my PIN
And I input the MyBlue PIN code "1689"
And I click the verify button
And I click continue button in the 4 months dialogue
When the Messaging preferences is turned on for Prescription Reminders and Messages
When I Click on Notification Icon
And I search for CVS Notification
And I verify the Shipping Confirmation Notification Verbiage "We’ve combined <NUM_OF_RX>  of your prescription orders into one package, shipped by <CARRIER_NAME> on <SHIP_DT>. Your tracking number is <TRACKING_NO>. Please note that your signature will be required."
And I sign off from MyBlue
