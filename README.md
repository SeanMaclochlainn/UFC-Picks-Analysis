# UFC-Picks-Analysis
An application that visualizes analysts picks from http://www.bloodyelbow.com/ and http://mmajunkie.com/, 
i.e. pages like this:

http://mmajunkie.com/2016/03/ufc-196-staff-picks-one-underdog-has-a-lot-more-support-than-you-might-think

or this: 

http://www.bloodyelbow.com/2016/3/4/11164328/ufc-196-mcgregor-vs-diaz-staff-picks-and-predictions

The data was manually copied and pasted from each page for each event. All of the data can be found in the folder Data in the rood directory. 

Finding pages for each event was done by using google search operators, e.g. 

site:bloodyelbow.com ufc 196 intitle:staff picks 

and in a similar fashion for mmajunkie.com

Betting odds were found in the same way, by searching for the event at https://www.bestfightodds.com/, copying and pasting this data into a file, and choosing from the SportBet column (randomly chosen)

Winners and losers of each fight were found using the same method of copy and pasting into a file, but from the wikipedia entry of the event, e.g. https://en.wikipedia.org/wiki/UFC_196#Results

When the program is executed (ConsoleApplication2\bin\Debug\ConsoleApplication2.exe) the output is then stored in the projects root directory, in an excel file called Results

Each analyst starts with €50, and bets €1 from their balance on each fight. At the end, their final balance is revealed, along with the percentage of correct picks. Blank fields are from where the analyst did not provide a pick for that event. 
 
