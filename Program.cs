/*
Task:
Finish describing ‘school’ domain. Define methods to add new pupils, organize classes, create schedule, update teacher’s information

*/

using School_project;

string firstName = null;
string lastName = null;
string classId = null;
string pupilId = null;
string userName = null;
string gender = null;
string pathPupil = "Pupils.csv";
string pathTeacher = "Teacher.csv";

Pupil pupil = new Pupil(firstName, lastName, classId, pupilId, userName, gender);

var classList = Pupil.ReadCsvFile(pathPupil);

Pupil.AddOrViewPupilData(classList, pathPupil); // Add or View pupil data by Pupil Id or by Class Id

Teacher.CreateNewTeacher(pathTeacher); 

Teacher.UpdateTeacherById(pathTeacher);



