// See https://aka.ms/new-console-template for more information

using ASVPwebMinjust.Service;

string pathWithPeople = "C:\\Users\\akaSh\\Downloads\\Telegram Desktop\\select_Top_200_DebtorName__DebtorCode_fr.csv";
string pathWithCode = "C:\\Users\\akaSh\\Downloads\\Telegram Desktop\\select_Top_200_VpNum_from_wdt__Enforceme.csv";
SerchingByFile personSerching = new SerchingByFile();
var resultsByPeople = personSerching.ReadFileWithPeople(pathWithPeople);
SerchingByFile codeSerching = new SerchingByFile();
var resultByCodes = codeSerching.ReadFileWithCode(pathWithCode);
CreatingExcelDoc excelDoc = new CreatingExcelDoc();
excelDoc.CreateExcel(resultsByPeople, "People", 1, "D:\\", "File");
excelDoc.CreateExcel(resultByCodes, "Codes", 2, "D:\\", "File");
Console.ReadLine();


