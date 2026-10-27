import { Component, DestroyRef, inject, OnInit, Signal, signal, ViewChild, WritableSignal } from '@angular/core';
import { GetClientResponse } from '../entites/responses/client-response';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

import {
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ClientsService } from '../services/clients/clients.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BehaviorSubject, map, pipe, switchMap, tap } from 'rxjs';
import { RouterLink } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddClientDialogComponent } from './dialogs/add-client-dialog/add-client-dialog.component';
import { DialogStatics, ErrorMessages } from '../statics/statics';
import { NotificationService } from '../services/notification/notification.service';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {MatSort, Sort, MatSortModule} from '@angular/material/sort';
import { WarningDialogComponentComponent } from '../common/dialogs/warning-dialog-component/warning-dialog-component.component';
import { WarningDialogData } from '../common/dialogs/warning-dialog-component/interface/warning-dialog-models';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
    RouterLink,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatSortModule
  ],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.scss'
})
export class ClientsComponent implements OnInit {

  readonly dialog = inject(MatDialog);
  
  private dialogRef: any;
  private readonly destroyRef = inject(DestroyRef);

  private subject = new BehaviorSubject(null);

  public disableAddButton: boolean = false;
  
  @ViewChild(MatSort) sort!: MatSort;
  
  searchFormControl = new FormControl('', []);

  displayedColumns: string[] = ['name', 'weight', 'height', 'email', 'actions'];

  public clients: GetClientResponse[] = [];
  public filteredClients: MatTableDataSource<GetClientResponse> = new MatTableDataSource<GetClientResponse>([]);

  constructor(
    public clientsService: ClientsService,
    private notificationService: NotificationService){}
  
  ngOnInit(): void {
    this.subject.pipe(
      takeUntilDestroyed(this.destroyRef),
      switchMap(() => {
        return this.clientsService.getClients();
      }),
      tap((data: GetClientResponse[]) => {
        this.clients = data;
        this.filteredClients.data = data;
      })
    ).subscribe();

    this.searchFormControl.valueChanges.subscribe((value) => { 
      this.filteredClients.data = this.clients.filter(client => {
        return client.email.toLocaleLowerCase().match(value?.toLocaleLowerCase()??'')
        || client.name.toLocaleLowerCase().match(value?.toLocaleLowerCase()??''); 
      })
    })
  }

  ngAfterViewInit() {
    this.filteredClients.sort = this.sort;
  }

  public openAddClientDialog(){
    this.disableAddButton = true;
    this.dialogRef = this.dialog.open(AddClientDialogComponent, {});

    this.dialogRef.afterClosed()
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe((message: string) => {
      this.disableAddButton = false;
      if(message === DialogStatics.SUCCESS)
      {
        this.subject.next(null);
      }
    });
  }

  public deleteClientDialog(id: string){
    let warningDialogData: WarningDialogData = {
      title: 'Delete Client',
      message: 'Are you sure you want to delete this client?',
      yesButtonText: 'Yes',
      noButtonText: 'No'
    };

    this.dialogRef = this.dialog.open(WarningDialogComponentComponent, {data: warningDialogData});

    this.dialogRef.afterClosed()
    .pipe(
      switchMap((message: string) => {
        if(message === DialogStatics.SUCCESS){
          return this.clientsService.deleteClient(id);
        }
        return [];
      }),
      takeUntilDestroyed(this.destroyRef))
    .subscribe({
      error: (err: any) => {
        this.notificationService.error(ErrorMessages.DeleteError + err.status);
      },
      next: (message: any) => {
        if(typeof(message) !== 'string'){
          this.notificationService.success(ErrorMessages.DeleteSuccess);
          this.subject.next(null);
        }
      }
    });
  }
}
