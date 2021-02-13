import { Exercise } from "./exercise";

export interface Program {
  programName: string;
  programDate: string;
  patientId: string;
  exercises: Exercise[]
}
