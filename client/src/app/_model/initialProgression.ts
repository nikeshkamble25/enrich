import { Program } from "./program";
import { Progression } from "./progression";

export interface InitialProgression {
  InitialProgressionReps: string;
  InitialrogressionPerWeek: string;
  InitialProgressionTimePeriod: string;
  progressions: Progression[];
  files: any[]
}
