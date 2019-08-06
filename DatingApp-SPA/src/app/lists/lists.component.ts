import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { Pagination, PaginatedResult } from '../_models/Pagination';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  likesParam: string;

  constructor(
    private authsService: AuthService,
    private userService: UserService,
    private route: ActivatedRoute,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit() {
    this.route.data.subscribe(data => {
      // tslint:disable-next-line: no-string-literal
      this.users = data['users'].result;
      // tslint:disable-next-line: no-string-literal
      this.pagination = data['users'].pagination;
    });
    this.likesParam = 'Likers';
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam).subscribe(
      (res: PaginatedResult<User[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
      },
      error => {
        this.alertifyService.error(error);
      }
    );
  }

  pagedChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }
}
