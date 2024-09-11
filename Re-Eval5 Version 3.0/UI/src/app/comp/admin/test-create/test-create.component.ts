import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr'; // Import ToastrService
import { ApiService } from '../../../core/services/api.service';
import { CreateExamDto, Exam, UpdateExamDto } from '../../../core/interfaces/exam.model';

@Component({
  selector: 'app-test-create',
  templateUrl: './test-create.component.html',
  styleUrls: ['./test-create.component.css']
})
export class TestCreateComponent implements OnInit {
  examForm!: FormGroup;
  editMode = false;
  selectedExam: Exam | null = null;
  exams: Exam[] = [];
  showModal = false;
  createdByUserId: string = '';
  minDate: string = new Date().toISOString().split('T')[0]; // Set minimum date to today

  constructor(
    private fb: FormBuilder,
    private examService: ApiService,
    private toastr: ToastrService // Inject ToastrService
  ) {
    this.createdByUserId = sessionStorage.getItem('userId') || '';
  }

  ngOnInit(): void {
    this.initializeForm();
    this.getAllExams();
  }

  initializeForm(): void {
    this.examForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      duration: [0, [Validators.required, Validators.min(1), Validators.max(1440)]],
      totalMarks: [0, [Validators.required]],
      passingMarks: [0, [Validators.required]],
      isRandmized: [false],
      createdByUserId: [''],
      isPublished: [false],
      createdDate: ['']
    }, { validators: [this.validateDates.bind(this), this.validatePassingMarks.bind(this)] });
  }

  validateDates(group: FormGroup): { [key: string]: boolean } | null {
    const startDate = group.get('startDate')?.value;
    const endDate = group.get('endDate')?.value;
    if (startDate && endDate && new Date(endDate) < new Date(startDate)) {
      return { 'endDateBeforeStartDate': true };
    }
    return null;
  }

  validatePassingMarks(group: FormGroup): { [key: string]: boolean } | null {
    const totalMarks = group.get('totalMarks')?.value;
    const passingMarks = group.get('passingMarks')?.value;
    if (totalMarks !== undefined && passingMarks !== undefined && passingMarks > totalMarks) {
      return { 'passingMarksGreaterThanTotalMarks': true };
    }
    return null;
  }

  getAllExams(): void {
    this.examService.getExams().subscribe({
      next: (data: Exam[]) => {
        this.exams = data;
      },
      error: () => {
        this.toastr.error('Failed to load exams. Please try again later.', 'Error');
      }
    });
  }

  openModal(exam?: Exam): void {
    this.showModal = true;
    if (exam) {
      this.editMode = true;
      this.selectedExam = exam;
      this.examForm.patchValue({
        title: exam.title,
        description: exam.description,
        startDate: exam.startDate,
        endDate: exam.endDate,
        duration: exam.duration,
        totalMarks: exam.totalMarks,
        passingMarks: exam.passingMarks,
        isRandmized: exam.isRandmized,
        createdByUserId: exam.createdByUserId,
        isPublished: exam.isPublished,
        createdDate: exam.createdDate
      });
    } else {
      this.editMode = false;
      this.examForm.reset();
    }
  }

  closeModal(): void {
    this.showModal = false;
    this.resetForm();
  }

  onSubmit(): void {
    if (this.examForm.invalid) {
      this.toastr.error('Please correct the errors in the form.', 'Form Invalid');
      return;
    }

    if (this.editMode && this.selectedExam) {
      const updateExamDto: UpdateExamDto = {
        title: this.examForm.value.title,
        description: this.examForm.value.description,
        startDate: this.examForm.value.startDate,
        endDate: this.examForm.value.endDate,
        isPublished: this.examForm.value.isPublished,
        duration: this.examForm.value.duration,
        totalMarks: this.examForm.value.totalMarks,
        passingMarks: this.examForm.value.passingMarks,
        isRandmized: true,
        createdByUserId: this.examForm.value.createdByUserId,
        createdDate: this.examForm.value.createdDate
      };
      this.examService.updateExam(this.selectedExam.examId, updateExamDto).subscribe({
        next: () => {
          this.toastr.success('Exam updated successfully!', 'Success');
          this.getAllExams();
          this.closeModal();
        },
        error: () => {
          this.toastr.error('Failed to update exam. Please try again later.',
            'Error');
          }
        });
      } else {
        const createExamDto: CreateExamDto = {
          title: this.examForm.value.title,
          description: this.examForm.value.description,
          startDate: this.examForm.value.startDate,
          endDate: this.examForm.value.endDate,
          duration: this.examForm.value.duration,
          totalMarks: this.examForm.value.totalMarks,
          passingMarks: this.examForm.value.passingMarks,
          createdByUserId: this.createdByUserId,
          isRandmized: this.examForm.value.isRandmized
        };
        this.examService.createExam(createExamDto).subscribe({
          next: () => {
            this.toastr.success('Exam created successfully!', 'Success');
            this.getAllExams();
            this.closeModal();
          },
          error: () => {
            this.toastr.error('Failed to create exam. Please try again later.', 'Error');
          }
        });
      }
    }
  
    togglePublishedStatus(exam: Exam): void {
      const updateExamDto: UpdateExamDto = {
        title: exam.title,
        description: exam.description,
        startDate: exam.startDate,
        endDate: exam.endDate,
        isPublished: !exam.isPublished,
        duration: exam.duration,
        totalMarks: exam.totalMarks,
        passingMarks: exam.passingMarks,
        isRandmized: exam.isRandmized,
        createdByUserId: exam.createdByUserId,
        createdDate: exam.createdDate
      };
      this.examService.updateExam(exam.examId, updateExamDto).subscribe({
        next: () => {
          this.toastr.success('Exam status updated successfully!', 'Success');
          this.getAllExams();
        },
        error: () => {
          this.toastr.error('Failed to update exam status. Please try again later.', 'Error');
        }
      });
    }
  
    toggleRandomizedStatus(exam: Exam): void {
      const updateExamDto: UpdateExamDto = {
        title: exam.title,
        description: exam.description,
        startDate: exam.startDate,
        endDate: exam.endDate,
        isPublished: exam.isPublished,
        duration: exam.duration,
        totalMarks: exam.totalMarks,
        passingMarks: exam.passingMarks,
        isRandmized: !exam.isRandmized,
        createdByUserId: exam.createdByUserId,
        createdDate: exam.createdDate
      };
      this.examService.updateExam(exam.examId, updateExamDto).subscribe({
        next: () => {
          this.toastr.success('Exam randomized status updated successfully!', 'Success');
          this.getAllExams();
        },
        error: () => {
          this.toastr.error('Failed to update randomized status. Please try again later.', 'Error');
        }
      });
    }
  
    resetForm(): void {
      this.examForm.reset();
      this.editMode = false;
      this.selectedExam = null;
    }
  }
  