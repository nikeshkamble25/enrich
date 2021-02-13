import { Program } from "./program";

export interface Patient {
    patientId: number;
    patientName: string;
    programList: Program[];
}
