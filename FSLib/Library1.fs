module FSAnalysis

#light

open System
open FSharp.Charting
open FSharp.Charting.ChartTypes
open System.Drawing
open System.Windows.Forms
//-----------------------------------------------------------------------------------------------------------------------------------------------

//
// Parse one line of CSV data:
//
//   Date,IUCR,Arrest,Domestic,Beat,District,Ward,Area,Year
//   09/03/2015 11:57:00 PM,0820,true,false,0835,008,18,66,2015
//   ...
//
// Returns back a tuple with most of the information:
//
//   (date, iucr, arrested, domestic, area, year)
//
// as string*string*bool*bool*int*int.
////-----------------------------------------------------------------------------------------------------------------------------------------------

let private ParseOneCrime (line : string) = 
  let elements = line.Split(',')
  let date = elements.[0]
  let iucr = elements.[1]
  let arrested = Convert.ToBoolean(elements.[2])
  let domestic = Convert.ToBoolean(elements.[3])
  let area = Convert.ToInt32(elements.[elements.Length - 2])
  let year = Convert.ToInt32(elements.[elements.Length - 1])
  (date, iucr, arrested, domestic, area, year)
//-----------------------------------------------------------------------------------------------------------------------------------------------

let private ParseOneIUCR (line : string) = 
  let elements = line.Split(',')
  let IUCR = elements.[0]
  let primary = elements.[1]
  let secondary = elements.[2]
  (IUCR, primary, secondary)
// //-----------------------------------------------------------------------------------------------------------------------------------------------
let private ParseOneArea (line : string) = 
  let elements = line.Split(',')
  let AreaCode =Convert.ToInt32(elements.[elements.Length - 2])
  let AreaName = elements.[1]
  (AreaCode, AreaName)
// //-----------------------------------------------------------------------------------------------------------------------------------------------
// Parse file of crime data, where the format of each line 
// is discussed above; returns back a list of tuples of the
// form shown above.
//
// NOTE: the "|>" means pipe the data from one function to
// the next.  The code below is equivalent to letting a var
// hold the value and then using that var in the next line:
//
//  let LINES  = System.IO.File.ReadLines(filename)
//  let DATA   = Seq.skip 1 LINES
//  let CRIMES = Seq.map ParseOneCrime DATA
//  Seq.toList CRIMES
////-----------------------------------------------------------------------------------------------------------------------------------------------

let private ParseCrimeData filename = 
  System.IO.File.ReadLines(filename)
  |> Seq.skip 1  // skip header row:
  |> Seq.map ParseOneCrime
  |> Seq.toList
  //-----------------------------------------------------------------------------------------------------------------------------------------------

  //To parse IUCR File
let private ParseIUCR filename = 
  System.IO.File.ReadLines(filename)
  |> Seq.skip 1  // skip header row:
  |> Seq.map ParseOneIUCR
  |> Seq.toList

  //-----------------------------------------------------------------------------------------------------------------------------------------------

    //To parse Areas File
let private ParseAreas filename = 
  System.IO.File.ReadLines(filename)
  |> Seq.skip 1  // skip header row:
  |> Seq.map ParseOneArea
  |> Seq.toList

  //-----------------------------------------------------------------------------------------------------------------------------------------------
//
// Given a list of crime tuples, returns a count of how many 
// crimes were reported for the given year:
//
let private CrimesThisYear crimes crimeyear = 
  let crimes2 = List.filter (fun (_, _, _, _, _, year) -> year = crimeyear) crimes
  let numCrimes = List.length crimes2
  numCrimes

  //-----------------------------------------------------------------------------------------------------------------------------------------------
let private countArrest crimes crimeyear=
  let arrests2 = List.filter (fun (_, _,arrest, _, _, year) -> (arrest = true) && (year = crimeyear)) crimes
  let numArrest = List.length arrests2
  numArrest
//-----------------------------------------------------------------------------------------------------------------------------------------------





//
// CrimesByYear:
//
// Given a CSV file of crime data, analyzes # of crimes by year, 
// returning a chart that can be displayed in a Windows desktop
// app:
//
let CrimesByYear(filename) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (_, _, _, _, _, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (_, _, _, _, _, year) -> year) crimes
  //
  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Counts: %A" counts
  printfn "Counts by Year: %A" countsByYear
  //
  // plot:
  //
  let myChart = 
    Chart.Line(countsByYear, Name="Total # of Crimes")
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl
//-----------------------------------------------------------------------------------------------------------------------------------------------

  //-----------------------------------------------------------------------------------------------------------------------------------------------

let ArrestByYears(filename) =
   //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename
  //
  let crimes = ParseCrimeData filename
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (_, _, _, _, _, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (_, _, _, _, _, year) -> year) crimes
  //
  let range  = [minYear .. maxYear]
  let arrests = List.map (fun year -> countArrest crimes year) range
  let arrestsByYear = List.map2 (fun year count -> (year, count)) range arrests

  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Arrests: %A" arrests
  printfn "Arrests by Year: %A" arrestsByYear
  printfn "Counts: %A" counts
  printfn "Counts by Year: %A" countsByYear
  //
  // plot:
  //
  let myChart =
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(arrestsByYear, Name="# of Arrests")
    ]) 
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl

//-----------------------------------------------------------------------------------------------------------------------------------------------
let private fuseLastTwoInTriple (one,two,three) =
    (one, two+three)

//let listToTuple list =
//    list |> (a,b,c) -> (a,b,c)
//
//let private checkIUCRtoString iucrList listLength =
//    match listLength = 1 with
//    |   true -> (null, null)
//    |   false -> (fuseLastTwoInTriple (listToTuple iucrList))

let private foundIUCR iucr iucrCode =
  let foundIUCRlist = iucr |> List.filter (fun (found,_,_) -> (found = iucrCode))
  foundIUCRlist
//  let returnString = checkIUCRtoString foundIUCRlist listLength
//  returnString
//-----------------------------------------------------------------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------------------------------------------------------------
let private countByIUCR crimes iucrCode yearCode =
  let foundIUCRlist = List.filter (fun (_,found,_,_,_,year) -> (found = iucrCode && year = yearCode)) crimes
  let iucrLength = List.length foundIUCRlist
  iucrLength

let private tupleToStringIUCR t1 = 
    let (a, b,c) = t1
    let tuple = (a, b, c)
    match tuple with
    |(a,b,c) -> b+c 

let cheatCode2 foundAreaList length =
    let list = List.filter (fun (_,_,_) -> 2 = 2) foundAreaList
    match length with
    | 0 -> ("", "unknown ", "crime code")
    | _ -> foundAreaList.Head 

//-----------------------------------------------------------------------------------------------------------------------------------------------
let CrimebyIUCR(filename1,filename2, textIUCR) =
   //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename1
  let crimes = ParseCrimeData filename1
  printfn "Calling IUCR: %A" filename2  
  let crimeCodes = ParseIUCR filename2
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (_, _, _, _, _, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (_, _, _, _, _, year) -> year) crimes
  //

  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  
  let foundIUCRlist = foundIUCR crimeCodes textIUCR
  let foundOrNotIUCR = List.length foundIUCRlist
  let foundIUCR = List.map (fun year -> countByIUCR crimes textIUCR year) range
  let IUCRByYear = List.map2 (fun year found -> (year, found)) range foundIUCR

 
  let printhelper = cheatCode2 foundIUCRlist foundOrNotIUCR
  let Printout = tupleToStringIUCR printhelper
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Counts: %A" counts
  printfn "Counts by Year: %A" countsByYear
  printfn "foundIUCR: %A" foundIUCR
  printfn "foundIUCR by Year: %A" IUCRByYear
  //
  // plot:
  //
  let myChart =
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(IUCRByYear, Name= Printout )
    ]) 
  let myChart2 = 
    myChart.WithTitle(filename1).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl
  //-----------------------------------------------------------------------------------------------------------------------------------------------


let private foundArea areaData areaGiven =
  let foundArealist  = List.filter (fun (_,found) -> (found = areaGiven)) areaData
  foundArealist

let private countByArea crimes areaCode yearCode =
  let foundArealist = List.filter (fun (_,_,_,_,found,year) -> (found = areaCode && year = yearCode)) crimes
  let areaLength = List.length foundArealist
  areaLength


//-----------------------------------------------------------------------------------------------------------------------------------------------
let private tupleToAreaCode t1 = 
    let (codefound, values) = t1
    let tuple = (codefound, values)
    match tuple with
    |(a,b) -> a 

let private tupleToString t1 = 
    let (codefound, values) = t1
    let tuple = (codefound, values)
    match tuple with
    |(a,b) -> b 


//let testTextvalidation listLength foundlist (number : string) = 
//    match listLength with
//    | 0 -> 0
//    | _ -> (tupleToAreaCode foundlist)

let cheatCode foundAreaList length =
    let list = List.filter (fun (_,_) -> 2 = 2) foundAreaList
    match length with
    | 0 -> (0, "unknown Area code")
    | _ -> foundAreaList.Head 


//-----------------------------------------------------------------------------------------------------------------------------------------------


//-----------------------------------------------------------------------------------------------------------------------------------------------
let CrimebyAreas(filename1,filename2, textArea) =
   //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename1
  let crimes = ParseCrimeData filename1
  printfn "Calling Area: %A" filename2  
  let areaCodes = ParseAreas filename2
  //
  let (_, _, _, _, _, minYear) = List.minBy (fun (_, _, _, _, _, year) -> year) crimes
  let (_, _, _, _, _, maxYear) = List.maxBy (fun (_, _, _, _, _, year) -> year) crimes
  //

  let range  = [minYear .. maxYear]
  let counts = List.map (fun year -> CrimesThisYear crimes year) range
  let countsByYear = List.map2 (fun year count -> (year, count)) range counts
  
  let foundArealist = foundArea areaCodes textArea
  let listLength = List.length foundArealist
  let returnList = cheatCode foundArealist listLength
  let codeFound = tupleToAreaCode returnList
 
  let condition range codeFound crimes  =
    if codeFound = 0 then List.map (fun year -> 0) range
    else  (List.map (fun year -> countByArea crimes codeFound year) range)


  let foundArea = condition range codeFound crimes 
  let AreaByYear = List.map2 (fun year found -> (year, found)) range foundArea
 // let Printout = test foundOrNotArea
  let printout = tupleToString returnList
  
  
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Counts: %A" counts
  printfn "Counts by Year: %A" countsByYear
  printfn "foundArea: %A" foundArea
  printfn "foundArea by Year: %A" AreaByYear
  //
  // plot:
  //
  let myChart =
    Chart.Combine([
                    Chart.Line(countsByYear, Name="Total # of Crimes")
                    Chart.Line(AreaByYear, Name= printout )
    ]) 
  let myChart2 = 
    myChart.WithTitle(filename1).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl
  //-----------------------------------------------------------------------------------------------------------------------------------------------
let private countByAreaTotal crimes areaCode  =
  let foundArealist = List.filter (fun (_,_,_,_,found,_) -> (found = areaCode )) crimes
  let areaLength = List.length foundArealist
  areaLength


let totalCrimes(filename,filename2) = 
  //
  // debugging:  print filename, which appears in Visual Studio's Output window
  //
  printfn "Calling CrimesByYear: %A" filename
  let crimes = ParseCrimeData filename
  printfn "Calling Area: %A" filename2  
  let areaCodes = ParseAreas filename2
 
  let ( minArea,_) = List.minBy (fun (area,_) -> area) areaCodes
  let ( maxArea,_) = List.maxBy (fun (area,_) -> area) areaCodes
  //
  
  let range2  = [minArea .. maxArea]

  let crimeCount = List.map(fun areaCode -> countByAreaTotal crimes areaCode) range2
  let countByAreaCodes = List.map2 (fun areaCode crimeCount -> (areaCode, crimeCount)) range2 crimeCount
  //
  // debugging: see Visual Studio's Output window (may need to scroll up?)
  //
  printfn "Counts Crimes: %A" crimeCount
  printfn "Counts by Area: %A" countByAreaCodes
  //
  // plot:
  //
  let myChart = 
    Chart.Line(countByAreaCodes, Name="Total Crimes by Chicago Area")
  let myChart2 = 
    myChart.WithTitle(filename).WithLegend();
  let myChartControl = 
    new ChartControl(myChart2, Dock=DockStyle.Fill)
  //
  // return back the chart for display:
  //
  myChartControl