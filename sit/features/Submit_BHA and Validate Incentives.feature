Given I opened the main page
And The 'MyBlue' page is successfully loaded
Given I input a username 'v2pvuser8'
Given I input a password 'Bigfun21!'
When I click submit button
When I click the link for a different verification way
And I choose use my PIN
And I input the MyBlue PIN code '3667'
And I click the verify button
And I click continue button in the for 4 months dialogue
When I click Health Tools tab
And I fill out the 'About You' section
And I fill out the 'Lifestyle' section
And I fill out the 'Well-being' section
And I fill out the 'Conditions' section
And I fill out the 'Lab Tests' section
And I fill out the 'Screenings' section
When I click 'Finish' button
Then The 'Blue Health Assessment Results' page is successfully loaded
Given I click the 'Wellness Card' tab from nav bar
And I click 'Card Activity' Link 
Then The 'ChipReward' Card Activity page is successfully loaded
And $50 incentive amount should diplay in Card Activity page
And I sign off from MyBlue