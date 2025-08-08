To load the website into your copy of Visual Studio 2022 follow these instructions...

How to Open and Run the HBPlayers Project in Visual Studio

1. Install Required Software
- Visual Studio 2022 (Community Edition or higher)
     Download: https://visualstudio.microsoft.com/vs/
- During installation, select the 'ASP.NET and web development' workload.

2. Clone the Repository from GitHub
- Open Visual Studio 2022.
- On the Start Window, select 'Clone a repository'.
- In the Repository Location field, paste:
     https://github.com/maxhodgess007/HBPlayers.git
- Choose a local folder (e.g., Documents\Visual Studio 2022\Projects).
- Click 'Clone'.

3. Open the Solution
- After cloning, Visual Studio will open the folder automatically.
- Locate and open the solution file:
     HerveyPlayersBooking/HerveyPlayersBooking.sln
- This will load both projects (web app and tests).

4. Restore Dependencies
- Go to Tools → NuGet Package Manager → Package Manager Console and run:
dotnet restore (Usually happens automatically when the project loads.)

5. Build the Solution
- Press Ctrl + Shift + B or go to Build → Build Solution.
- Ensure 'Build succeeded' appears in the Output window.

6. Run the Website
- Set HerveyPlayersBooking as the Startup Project
- 1st time only... Select drop down menu next to green arrow and select IIS Express
- From now on you can just press the green arrow IIS Express button to start the webpage

++++++++++++++++++++

If you get exceptions when you try and make a booking this should fix it. Regards, Max.

++++++++++++++++++++

Why it happens... When you clone/fork a project, the *.db file is not included in Git (ignored by .gitignore).

EF Core migrations that create tables aren’t automatically applied after cloning.

How to fix it... 

1. Open Visual Studio

2. Open Package Manager Console in Visual Studio

	Tools > NuGet Package Manager > Package Manager Console

2. Run these commands in Powershell

	Add-Migration InitialCreate

	Update-Database

This will create the SQLite database and build the Bookings table.
