import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from './_model/patient';
import { Program } from './_model/program';
import { Exercise } from './_model/exercise';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }
  readonly APIUrl = 'http://localhost:4000/api/';
  getAllPatients(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + 'Patient/GetAllPatients');
  }
  // tslint:disable-next-line:typedef
  UploadImage(File: any) {
    return this.http.post(this.APIUrl + 'ImageVideoUpload/UploadImage', File);
  }
  // tslint:disable-next-line:typedef
  addExerciseProgram(exercise: any, imageVideoIds: any) {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(this.APIUrl + 'Excercise/AddExcercise?imageVideoIds=' + imageVideoIds, exercise, { headers: header });
  }
  getImageVideoByImageId(imageId: any): Observable<any> {
    return this.http.get<String>(this.APIUrl + 'ImageVideoUpload/GetImageVideoByImageId?imageId=' + imageId);
  }

  getPatient(id: String): Observable<Patient> {
    return this.http.get<any>(this.APIUrl + 'Patient/GetPatientsById?patientId=' + id);
  }
  getPatientProgram(id: String): Observable<Program> {
    return this.http.get<any>(this.APIUrl + 'ProgramExcercise/GetProgramByPatientId?patientId=' + id);
  }
  getPatientExerciseByProgram(id: String): Observable<Exercise> {
    return this.http.get<any>(this.APIUrl + 'Excercise/GetExerciseByProgramId?programId=' + id);
  }
  AddProgram(program: any) {
    debugger;
    return this.http.post(this.APIUrl + 'ProgramExcercise/AddProgram', program);
  }
}
