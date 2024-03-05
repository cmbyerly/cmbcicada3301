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
- The text files are just too large and numerous to look through cleanly.  I can do this better with a database.