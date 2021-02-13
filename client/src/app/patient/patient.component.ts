import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit {


  constructor(private ef: FormBuilder, private service: SharedService, public dialogRef: MatDialogRef<PatientComponent>) { }
  fileList: any[] = [];
  exerciseForm!: FormGroup;
  initialProgression = [];
  progression = [];
  @Input() Patient: any;
  ExerciseId!: string;
  PatientName!: string;
  Exercise!: string;
  Date!: string;
  PhotoFileName!: string;
  PhotoFilePath?: string;
  urls: any[] = [];

  ExerciseList: any = [];
  userSubmitted!: boolean;

  ExerciseDetails: any = [];
  ImageIds: any = [];


  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createExerciseForm();
    this.addNextExercise();
  }
  // tslint:disable-next-line:typedef
  createExerciseForm() {
    this.exerciseForm = this.ef.group({
      ProgramName: ["Program Name", Validators.required],
      ProgramDate: ["1990-10-10", [Validators.required]],
      GoalsObjectives: ["test", [Validators.required]],
      exercises: this.ef.array([])
    });
  }

  addExercise() {
    // // this.ExerciseObj['excerciseId'] = 0;
    // // this.ExerciseObj['excerciseName'] = this.exerciseForm['ExerciseName'];
    // // this.ExerciseObj['reviewDate'] = this.exerciseForm['ReviewDate'];
    // // this.ExerciseObj['description'] = this.exerciseForm['Description'];
    // this.ExerciseDetails = this.exerciseForm.value;
    // // tslint:disable-next-line:no-string-literal
    // delete this.ExerciseDetails['ProgramName'];
    // // tslint:disable-next-line:no-string-literal
    // delete this.ExerciseDetails['ProgramDate'];
    // // tslint:disable-next-line:no-string-literal
    // delete this.ExerciseDetails['GoalsObjectives'];
    // this.ImageIds = localStorage.getItem('imageId');
    // this.service.addExerciseProgram(this.ExerciseDetails, this.ImageIds).subscribe((ExcerciseId: any) => {
    //   this.ExerciseId = ExcerciseId;
    //   console.log(this.ExerciseId);
    // });
    // this.userSubmitted = true;
    // localStorage.removeItem('imageId');


    // const formData: FormData = new FormData();
    // for (let index = 0; index < this.fileList.length; index++) {
    //   const f = this.fileList[index];
    //   formData.append('files', f.file, f.file.name);
    // }
    // formData.append('exerciseId', "1");
    // this.service.UploadImage(formData).subscribe((data: any) => {
    //   localStorage.setItem('imageId', data);
    // });

    // this.ExerciseObj['excerciseId'] = 0;
    // this.ExerciseObj['excerciseName'] = this.exerciseForm['ExerciseName'];
    // this.ExerciseObj['reviewDate'] = this.exerciseForm['ReviewDate'];
    // this.ExerciseObj['description'] = this.exerciseForm['Description'];
    this.ExerciseDetails = this.exerciseForm.value;
    console.log(this.exerciseForm.value.exercises[0]);
    debugger;
    // tslint:disable-next-line:no-string-literal
    delete this.ExerciseDetails['ProgramName'];
    // tslint:disable-next-line:no-string-literal
    delete this.ExerciseDetails['ProgramDate'];
    // tslint:disable-next-line:no-string-literal
    delete this.ExerciseDetails['GoalsObjectives'];
    this.ImageIds = localStorage.getItem('imageId');
    this.service.addExerciseProgram(this.ExerciseDetails, this.ImageIds).subscribe((ExcerciseId: any) => {
      this.ExerciseId = ExcerciseId;
    });
    this.userSubmitted = true;
    localStorage.removeItem('imageId');
  }

  // UploadImageFile(event: any) {
  //   const file = event.target.files[0];
  //   const formData: FormData = new FormData();
  //   formData.append('file', file, file.name);
  //   this.service.UploadImage(formData).subscribe((data: any) => {
  //     localStorage.setItem('imageId', data);
  //     const imageId = localStorage.getItem('imageId');
  //     this.service.getImageVideoByImageId(imageId).subscribe((image) => {
  //       this.PhotoFilePath = image[0];
  //       this.urls.push(image[0]);
  //     }, err => console.log('http error', err));
  //   });
  // }

  // UploadImage(event: any, exId: any) {
  //   const file = event.target.files[0];
  //   var reader = new FileReader();
  //   reader.readAsDataURL(event.target.files[0]);
  //   reader.onload = (_event) => {
  //     this.fileList.push({
  //       file: file,
  //       imageUrl: reader.result?.toString(),
  //       exerciseId: exId
  //     });
  //   }
  // }

  UploadImageFile(event: any, exId: any) {
    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        var reader = new FileReader();
        reader.onload = (e: any) => {
          this.urls.push(e.target.result);
        }
        reader.readAsDataURL(event.target.files[i]);
        this.fileList.push({
          file: event.target.files[0],
          exerciseId: exId
        });
        console.log(this.fileList);
      }
    }
  }

  updatePatient() {

  }
  get f() {
    return this.exerciseForm.controls;
  }
  onReset() {
    this.userSubmitted = false;
    this.exerciseForm.reset();
  }
  addProgression() {
    return this.ef.group({
      progressionReps: [1, Validators.required],
      progressionPerWeek: [2, Validators.required],
      ProgressionTimePeriod: [3, Validators.required]
    });
  }
  addExerciseForm() {
    return this.ef.group({
      exerciseName: ["test ex", Validators.required],
      reviewDate: ["1990-10-10", [Validators.required]],
      description: ["test", Validators.required],
      initialProgression: this.ef.group({
        initialProgressionReps: ["1", Validators.required],
        initialProgressionPerWeek: ["1", Validators.required],
        initialProgressionTimePeriod: ["1", Validators.required],
        progressions: this.ef.array([])
      })
    });

  }
  addNextProgression(exIndex: any) {
    this.getProgression(exIndex).push(this.addProgression());
  }
  getProgression(exIndex: any) {
    return (<FormArray>((<FormGroup>this.exerciseForm.get('exercises')?.get(exIndex.toString())).get("initialProgression"))?.get('progressions'));
  }
  deleteProgression(exIndex: any, index: any) {
    (<FormArray>(<FormGroup>this.exerciseForm.get('initialProgression')).get('progressions')).removeAt(index);
  }

  addNextExercise() {
    (<FormArray>this.exerciseForm.get('exercises')).push(this.addExerciseForm());

  }
  getExercise() {
    return (<FormArray>this.exerciseForm.get('exercises'));
  }
  saveProgram() {
    const fList: File[] = [];
    const formData: FormData = new FormData();
    const programData = this.exerciseForm.value;
    programData.patientId = sessionStorage.getItem("pid");
    // if (programData.exercises) {
    //   for (let index = 0; index < this.fileList.length; index++) {
    //     debugger;
    //     const file = this.fileList[index];
    //     if (!programData.exercises[file.exerciseId].files) {
    //       programData.exercises[file.exerciseId].files = [];
    //     }
    //     programData.exercises[file.exerciseId].files.push(file.file);
    //     fList.push(file.file);
    //   }
    // }
    formData.append("patientId", "1");
    console.log(programData);

    this.service.AddProgram(formData).subscribe((ExcerciseId: any) => {
      this.ExerciseId = ExcerciseId;
      console.log(this.ExerciseId);
    });
  }

  getImageVideo(exId: any) {
    return this.fileList.filter(x => x.exerciseId == exId);
  }
}
