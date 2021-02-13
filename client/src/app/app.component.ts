import { Component } from '@angular/core';
import { SharedService } from './shared.service';
import { Patient } from './_model/patient';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { PatientComponent } from './patient/patient.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  showFiller = true;
  PatientList: any = [];
  Patient!: Patient;
  PatientProgramList: any = [];
  constructor(private service: SharedService, public dialog: MatDialog) {

  }
  ngOnInit(): void {
    this.refreshPaitentList();
  }
  refreshPaitentList() {
    this.service.getAllPatients().subscribe(data => {
      this.PatientList = data;
      this.displayPatient(this.PatientList[0].patientId);
    });
  }
  displayPatient(pNumber: any) {
    sessionStorage.setItem("pid", pNumber);
    this.service.getPatient(pNumber).subscribe((data: Patient) => {
      this.Patient = data;
      this.displayPatientProgram(pNumber);
    });
  }
  displayPatientProgram(pNumber: any) {
    this.service.getPatientProgram(pNumber).subscribe(data => {
      this.PatientProgramList = data;
      for (let index = 0; index < this.PatientProgramList.length; index++) {
        const program = this.PatientProgramList[index];
        this.service.getPatientExerciseByProgram(program.programId).subscribe(exdata => {
          this.PatientProgramList[index].excercise = exdata;
        });
      }
    });
  }
  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = ["myclass"];
    dialogConfig.width = "800px";
    dialogConfig.position = {
      'top': '10px'
    };
    this.dialog.open(PatientComponent, dialogConfig);
  }
}
