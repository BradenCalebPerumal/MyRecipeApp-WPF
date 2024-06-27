Name: Braden Caleb Perumal 
Student Number: ST10287165
My Recipe App

Contents:
1)	          Introduction---------------------------------------------------
2)	          Requirements-------------------------------------------------
3)	          How to Apply-------------------------------------------------
4)	          Application Overview--------------------------------------
5)	          Architecture--------------------------------------------------
6)	          Functionality--------------------------------------------------
7)	          Non-Functional Requirements---------------------------
8)	          Change Log----------------------------------------------------------
9)	          FAQ's----------------------------------------------------------
10)	          Licencing----------------------------------------------------------
11)	          Plugins----------------------------------------------------------
12)	          Credits----------------------------------------------------------
13)	          GitHub Link----------------------------------------------------
14)	          References----------------------------------------------------



1)	Introduction:
The My Recipe App has evolved into a comprehensive WPF-based application, enhancing user experience with a graphical interface while retaining its core functionalities such as adding, viewing, scaling, and deleting recipes. This version introduces an interactive and visually appealing interface complete with emojis and color-coded text, streamlined ingredient management, and customizable recipes for varying serving sizes, catering to both individual users and culinary enthusiasts seeking an efficient way to manage their recipe

2)	Requirements:
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6" />
    </startup>
</configuration>

3)	How to apply

	Download the source code or clone the repository.
	Launch Microsoft Visual Studio IDE and open solution.
	Create and launch the program. 
	Comply with the directions given on the command line to enter the recipes details, to view it, scale it or delete it.

4)	Application Overview
The purpose of this application:
The purpose of the program is to facilitate effective recipe management for users. Users are able to view, edit, eradicate and store recipes. Those who cook regularly and require an orderly method to manage several recipes, including their ingredients and preparation steps, could find this application extremely useful.

Key Features
Recipe Management: Users are able to upload new recipes and provide information about them, including the name of the recipe, how many servings it makes, the materials required (along with amounts and measurements), and step-by-step preparation instructions. It is also possible to review and remove recipes.
Handling Ingredients: Extensive data on ingredients, such as amounts and measurement units, may be entered and saved for every recipe. This features support for many units, including milliliters, tablespoons, kilos, and grams, which increases the application's adaptability to handle a range of cooking methods.
Recipe Scaling: Based on preferred serving sizes, users may scale recipes using this application's capability. It is easier to modify recipes for varying serving sizes when users have the option to half, double, or treble the amounts of components in a recipe.
Interactive Console Interface: To make the interaction visually appealing and easy to use, the program makes use of a console-based interface that includes boxes and color-coded text. Emojis are utilized to improve reader engagement and readability.
Single Recipe Focus: The application's present architecture limits user freedom while streamlining administration by handling only one recipe at a time. This function guarantees targeted and uncomplicated recipe administration, making it perfect for presentations or instructional uses.
Unique Selling Points :
User Interface: Unlike conventional text-only console apps, the console interface is visually appealing and easy to use thanks to the usage of emojis, colorful text, and text boxes.

Recipe Scaling Functionality: One notable feature is the ease with which recipes may be scaled to suit varying serving requirements. This allows users to make adjustments to recipes without having to manually recalculate component amounts.

Built-in Validation: By preventing frequent input mistakes, the application's checks and validations make sure that all inputs are acceptable and correct, improving both its robustness and user experience.
Provision for Future Improvements: The design of the program permits for potential future add-ons, including the ability to manage several recipes at once, which might be enabled by modifying the existing restrictions.

5)	Architecture
This program has a simple, but well-organized architecture. It comprises of a major entry point in the Program class that takes the user to the RecipeFunctions class's main menu after initializing the program. As the main controller, this class manages user inputs and controls how the application flows via actions like adding, scaling, and removing recipes.
The Recipe, Ingredients, and ScalingRecipe classes make up the application's data models as they include the methods and data attributes required to manage recipes and their details. The application's structure resembles the MVC pattern, however without expressly using a sophisticated design style. Models represent data, a controller represents logic, and the view is console output.
Improvements may include employing dependency injection for more flexibility and simpler testing, adding a persistence layer for data storage, and clearly separating concerns in accordance with the MVC design. All in all, this configuration offers a simple and efficient foundation for a recipe management program, focusing on easy-to-use interaction with an improved console interface.

6)	Functionality:
This application complies with the following criteria:
a)	Single Recipe Entry: This makes data entry easier because the user can only enter information for one recipe at a time.
b)	Detailed Recipe Display: To ensure user clarity and easy of comprehension, the program provides the whole recipe in a clean and organized manner, including the number of ingredients, their names, amounts, and measurement units.
c)	Recipe Scaling: The recipe can be scaled by a multiple of two, three, or five. The user may effortlessly modify the recipe to suit varying serving sizes thanks to this capability.
d)	Revert to original quantities: This feature gives the user flexibility in managing recipes by allowing them to return the ingredient amounts back to their initial levels after scaling.
e)	Deletion and New Start: By allowing the user to input a new recipe and erase all previous data, the program is kept manageable and free of superfluous information.
f)	In-memory Data Storage: Because the program only stores data in memory while it is running, user data is not retained after it has been closed. This indicates that no permanent storage, such as a file system or database, is used.

7)	Non-Functional Requirements

a)	Compliance with Coding Standards: The program adheres to generally recognized coding standards, which likely ensures readability, maintainability, and consistent documentation of the codebase. This would include using meaningful variable names, following method naming conventions, and commenting on the code to describe functionality and logic.
b)	Use of Classes for Functionality: The application implements necessary functionalities using classes, indicating an object-oriented approach. This encapsulates data and behaviours within objects, potentially making the code more modular, reusable, and easier to manage.
c)	Lists for Data Storage: The application employs lists to store components and procedures, indicating a choice for in-memory data structures to manage data. This choice makes caters for dynamic storage as this data can be modified, scaled as well as be unrestricted in terms of limit. This likely refers to the use of arrays or similar data structures to maintain the state of the application while it's running.

8) Changelog
Transition from Part 1 to Part 2:

Multiple Recipe Management:

Previously: The application was limited to handling a single recipe at a time.
Update: Implemented functionality to handle multiple recipes, allowing users to manage a diverse cookbook within the application.
Recipe Identification and Organization:

Previously: Recipes were not identifiable by unique names and were displayed in no particular order.
Update: Added functionality to name recipes. Recipes are now displayed to the user in alphabetical order by name for easy navigation and selection.
Enhanced Nutritional Tracking:

Previously: Ingredients were managed without detailed nutritional information.
Update: Users can now input and view detailed nutritional information for each ingredient, enhancing the app’s utility for dietary management.
Calorie Calculation and Notification:

Previously: The application did not support calculating or alerting users about the total calories of a recipe.
Update: Introduced features for automatic calorie calculation per recipe. Added notifications to alert users when total calories exceed a predefined limit (e.g., 300 calories), promoting health-conscious usage.
User Interface Enhancements:

Previously: User interactions were primarily text-based and static.
Update: Enhanced the user interface to be more interactive, with dynamic updates and feedback when actions are performed, such as adding or scaling recipes.
Technical Enhancements:

Use of Generic Collections:
Previously: Data was stored using arrays, which limited flexibility.
Update: Transitioned to using generic collections like List<T>, enhancing performance and data manipulation capabilities.
Introduction of Unit Testing:
Previously: No unit testing was in place for functionality verifications.
Update: Introduced unit tests, particularly for new functionalities like calorie calculations, to ensure reliability.
Transition from Part 2 to Part 3 (Current Version):

Introduction of WPF and MVVM Architecture:

Previously: The application was based on a console interface with limited user interaction capabilities.
Update: Transitioned to a Windows Presentation Foundation (WPF) framework, utilizing the Model-View-ViewModel (MVVM) architectural pattern. This significant change enhances user experience through a graphical interface, allowing for more intuitive and responsive interactions.
Graphical and Interaction Overhaul:

Previously: Functionalities like adding, viewing, or scaling recipes were performed through a console-based text interface.
Update: These functionalities are now handled through graphical forms and controls, making the processes more engaging and easier to use. Enhanced data binding and event-driven programming improve the application's responsiveness and efficiency.
Data Persistence Options:

Previously: The application only supported session-based data storage with no long-term persistence.
Update: Laid the groundwork for future integration with database systems or file-based storage, improving data management and enabling persistent storage options.
Accessibility and Customization Enhancements:

Previously: Limited customization options available for user interface settings.
Update: Introduced options to customize the application settings, including themes, fonts, and layout preferences, accommodating user accessibility needs and personal preferences.
Comprehensive Error Handling and Validation:

Previously: Basic error handling was implemented with minimal user feedback on input errors.
Update: Advanced error handling mechanisms and detailed validation feedback have been integrated, ensuring users are well-informed of any input mistakes or system errors, thereby enhancing the overall usability and robustness of the application

9) How to use application 

Starting the Application:
Open the Application:
- Locate and double-click the Recipe App icon on your desktop or search for it in the Start menu.
Initial Setup:
- Upon first launch, the application may prompt you to set up initial preferences such as language and theme. Choose your preferences to customize your user experience.
- Navigating the Main Menu:
- The main menu is the hub from which all functionalities of the app are accessible. It typically includes options like Add Recipe, Search Recipe, Scale Recipe, Delete Recipe, and Exit.
- Adding a New Recipe:
- Access the Add Recipe Feature:
- From the main menu, select 'Add Recipe' to navigate to the recipe input form.
Enter Recipe Details:

- Fill in the recipe name, number of servings, and a brief description.
- Add ingredients by specifying the name, quantity, and measurement unit for each.
- Describe the cooking steps in the provided text area. Each step can be added sequentially using the 'Add Step' button.

Submit the Recipe:
- After entering all details, review your entries and click the 'Submit' or 'Save Recipe' button to store the recipe in the database.
- Searching for Recipes:
- Access the Search Feature:
- Select 'Search Recipe' from the main menu to open the search interface.

Set Search Parameters:
- You can search by recipe name, ingredient, or calories. Select the appropriate parameter and enter your search query in the search box.

View Search Results:
= The results will be displayed in a list format. Click on a recipe to view detailed information.

Scaling a Recipe:
- Access the Scale Recipe Feature:
- Choose 'Scale Recipe' from the main menu.

Select a Recipe to Scale:
- Select a recipe from your list of saved recipes.

Adjust Serving Size:
- Input the new number of servings or the scale factor (e.g., 2 for doubling the recipe). The app will automatically adjust the ingredient quantities based on your input.

Deleting a Recipe:
- Navigate to Delete Recipe:

- From the main menu, select 'Delete Recipe.'

Select the Recipe to Delete:
- Choose the recipe you wish to remove from your recipe list.

Confirm Deletion:

- Confirm that you want to delete the selected recipe. This action is irreversible.

Exiting the Application:
- To safely exit the Recipe App, select the 'Exit' option from the main menu. Ensure that all your data is saved before exiting to avoid losing any changes.

Tips for Effective Use:

- Regularly Save Your Data: Make use of the save features frequently to avoid data loss.

Customize Your Experience: Adjust settings like theme and display options to enhance readability and ease of use.

Use Help Resources: For further assistance, refer to the help section within the app for detailed guides and troubleshooting support.

10) FAQ's

FAQ:
Q1: What should I do if I encounter an error during recipe input?
A1: If you encounter errors during recipe input, ensure that you are following the input format strictly as prompted. For example, quantities should be numeric, and units must match the available options. If issues persist, restart the application to clear any potential input errors.

Q2: How can I print the recipe details to share with others?
A2: The current version of the application does not directly support printing. However, you can select and copy the text from the console window and paste it into a text file or word processor, then print from there.

Q3: Is there a way to back up my recipes before closing the application?
A3: Since the application does not save data to persistent storage, consider manually transcribing important recipes to a digital document or paper as a backup. We recommend keeping a separate document for recipes you don't want to lose.

Q4: The application is unresponsive. What should I do?
A4: If the application becomes unresponsive, try closing the console window and reopening the application. If unresponsiveness persists, check your system's task manager to ensure that no background processes are affecting its performance.

Q5: Can I add photos to my recipes?
A5: The console-based nature of the application does not support adding photos. To include photos, you might consider maintaining a separate digital or physical recipe book where you can store photos alongside the written details.

Q6: How do I update the application or check for updates?
A6: Since the application does not include a built-in update mechanism, you should check the GitHub repository for any updates or new versions. Download and replace the application files with the new version as needed.

Q7: I've noticed a bug. How can I report it?
A7: Bugs can be reported directly through the GitHub issues page of the repository. Please provide a detailed description of the bug, steps to reproduce it, and screenshots if possible. This information will help in diagnosing and fixing the issue more efficiently.

Q8: Are there keyboard shortcuts to speed up my workflow?
A8: The application primarily relies on menu selections and does not support keyboard shortcuts in its current form. However, you can navigate quicker by becoming familiar with the menu numbers and entering your choices more rapidly.

Q9: What should I do if the application does not start?
A9: Ensure that .NET Core 3.1 or later is installed on your system. Check the application's build configuration and ensure that there are no build errors.

10) Licencing
This project, "My Recipe App," is made available under the MIT License. The terms of the license allow for free use, modification, and distribution, under the conditions that the original author is credited and the software is distributed under the same license.
Why the MIT License?
The MIT license is a popular open-source license due to its simplicity and permissiveness. It allows users great freedom with the software but requires that the license and copyright notice are preserved. It is suitable for small to large projects that wish to allow wide use and adaptation of the software without complex legal restrictions.

If you prefer a different license model, or if your project has specific needs (like compatibility with other licenses or corporate policies), you might consider alternatives like the GPL (which requires derivatives to also be open-source) or commercial licenses if you want to restrict how the software is used commercially.

11) Plugins

This application does not currently use any external plugins. All functionalities are built-in and fully integrated within the application codebase.

12)	Credits:
This program has been entirely built and tested by Braden Caleb Perumal (ST10287165).

13)	GitHub Link:
https://github.com/VCWVL/prog6221---programming-2a---poe-CalebPerumal28.git
Screenshot of GitHub Representation is pasted in the MSW document.

14)	References:

BroCode, n.d. C# Full Course for free. [Online] 
Available at: https://www.youtube.com/watch?v=wxznTygnRfQ&t=10600s&ab_channel=BroCode
BROCODE, n.d. C# tutorial for beginners. [Online] 
Available at: https://www.youtube.com/watch?v=r3CExhZgZV8&list=PLZPZq0r_RZOPNy28FDBys3GVP2LiaIyP_&ab_channel=BroCode
Christensen, M., n.d. Creating a List of Lists in C#. [Online] 
Available at: https://stackoverflow.com/questions/12628222/creating-a-list-of-lists-in-c-sharp
geeksforgeeks, n.d. C# | Constructors. [Online] 
Available at: https://www.geeksforgeeks.org/c-sharp-constructors/
Slayden, G., n.d. How to convert emoticons to its UTF-32/escaped unicode?. [Online] 
Available at: https://stackoverflow.com/questions/44728740/how-to-convert-emoticons-to-its-utf-32-escaped-unicode
