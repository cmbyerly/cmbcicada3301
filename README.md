# cmbcicada3301
This is a repo where I am testing out some stuff regarding Liber Primus by Cicada 3301.

# Thoughts
I have known in the past that Cicada 3301 has used spaces in some of their puzzles.  I haven't seen anything out there that has looked at that kind of stuff with Liber Primus outside of outguess.  Someone has probably done this before.

This may amount to nothing?

# Methodology
I have written a C# program to do this with ImageMagick and ClosedXML to generate the workbook with the information.

## Color Count per line.
- Looping through the files and getting the count of a certain color and then it will change color, get the number, etc.
- Output will be a Run<datetimestamp>.xlsx file.  If you want to save it off, be sure to copy.  It removes previous runs.

## Observations
### Round 0
- Width and height were pretty much uniform.  Nothing special about them.
- Got basic information, but showed pages with 2 or 3 colors to have in the hundreds of colors.  I think there could be something more there.

### Round 1
- Updated program to do the color thing.  So basically, on a line it gets the count of colors, then when it changes mark the count for it on a column for that row in the excel.
- The run is pretty fast, but it uses a lot of memory.
- Big ol' spreadsheet of numbers.  Switched to text files.
- Round 1 folder - output with char counts to strings...
- Checked to make sure that the characters amount to something?
- Ok, there was a lot of data and I needed to go to bed.  More to come???

### Round 2
- Going to try color counts in seperate counters.
- The colors are in seperate counters on the serial occurence within the file.
- This is in the round 2 folder.
- Though I do find it interesting that there are a lot of small numbers and ones in the file.  I do wonder if there really could be some binary actually embedded within the file?