Google Code Project URL:
http://code.google.com/p/flash-validators



Overview:

Two versions of the Flash data validators are provided within this project:

The as2validators folder contains ActionScript 2.0 versions of common data
validation routines.

The as3validators folder contains ActionScript 3.0 versions of common data
validation routines.

The as2validator and as3validator code is almost identical other than minor
variations for their respective languages.  While the code is compatible
with Flash Professional and Flex environments, Flex developers should also
look at the validators provided within the Adobe Flex 3 SDK to determine if
the SDK validators will better suit their needs.

The code provided here demonstrates a basic, sample framework for data
validation. It is expected that developers will take the concepts from this
sample code and customize it for their implementations and data types. You
should review the logic of each function to determine if it is sufficient
for your environment before incorporating it into your code.

Each directory contains a docs folder that contains the JavaDoc information
for the validation classes.



Usage:

Execute either the as2DataValidation_Test.swf or the as3DataValidation_Test.swf
to see a sample application that tests the data validation functions.  The
instructions below apply to both SWF files.

The "Input to Validate" input field is where you insert the string to test

The "Available Functions" list on the right-hand side contains all of the data
validation functions available within this library.  Click on the name of the
data validation function in order to test the input in the "Input to Validate"
field and see the result in the Output field.

The "Domain for HTTP URL Validators" input fields are for when you are using
the "Validate HTTP URL" or "Validate HTTPS URL" validators.  It is ignored for
all other validators.

The "Min" and "Max" input fields under "Input in Range" are for entering the
minimum and maximum values for an "Is Integer In Range" validator test.  The
integer in the "Input to Validate" field will be tested to determine if it is
within the specificed range.

The "Output" Pane shows the result of the test. It will show true when the
input matches the requirements of the validators.  It will show false when the
input does not meet the requirements of the validators.  It will also provide
information on the cause of the error.