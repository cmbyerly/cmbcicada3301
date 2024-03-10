# cmbcicada3301
This is a repo where I am testing out some stuff regarding Liber Primus by Cicada 3301.

# Thoughts
I have known in the past that Cicada 3301 has used spaces in some of their puzzles.  Someone has probably done this before.

This will most likely amount to nothing?

# Methodology
I have written a C# program to do this with ImageMagick to generate the files with the information.

# Required Software
- SQL Server Management Studio
- Docker (Podman if you want to do the work)
- Visual Studio or Visual Studio Code
- .Net Core 8
- I know a lot of the solvers use Python, but I just prefer C# since I use it most of the time.

# Table Schema
CODE_SET_TABLES - This is the table the with ASCII and ANSI sets for quicker lookup.

# Color Counts
- Looping through the files and getting the count of a certain color and then it will change color, get the number, etc.
- The output will be a SQL server instance that runs in Docker (at least on my box.)

## Observations
### Revision 0
- Width and height were pretty much uniform.  Nothing special about them.
- Got basic information, but showed pages with 2 or 3 colors to have in the hundreds of colors.  This is just anti-aliasing.

### Revision 1
- The run is pretty fast, but it uses a lot of memory.
- Checked to make sure that the characters amount to something?
- Ok, there was a lot of data and I needed to go to bed.  More to come???

### Revision 2
- Going to try color counts in separate counters.
- The colors are in separate counters on the serial occurrence within the file.
- Though I do find it interesting that there are a lot of small numbers and ones in the file.  I do wonder if there really could be something embedded within the file?

### Revision 3
- Broke the "tests" out into their own classes.
- Simplified the program.cs file
- Wrote color report.
- Also since everything writes with a round # text file, I will just consolidate to one output folder.

### Revision 4
- Changing out the text files for a SQL Server.
- The text files are just too large and numerous to look through cleanly.  I can do this better with a database (in progress).
- Added reverse bytes test after looking at the 05 image in a hex editor.
- You can reverse the bytes on image 05 and it will still produce a good image.  Weird.
- Getting the diff bits produces: "3237322002020203133382020202020333431202020202031333120202020203135310a33636202020202031393920202020203133302020202020333300202020203138a323236200020203343520200202039312020202020203343520200202033360a31382202020202033330020202020313330202020202031393920202020203336360a135312020202020313331202020202033343120202020203133382020202020337320a<LF><LF>"
- And all that is documented in https://uncovering-cicada.fandom.com/wiki/What_Happened_Part_1_(2014)
- That is a derp moment for me.