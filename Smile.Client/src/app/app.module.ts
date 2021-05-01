import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { InterceptorProvider } from './services/interceptor/interceptor.service';
import { NotifierModule } from 'angular-notifier';
import { customNotifierOptions } from 'src/environments/environment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { routes } from './routes';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';
import { MatBadgeModule } from '@angular/material/badge';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import localeEn from '@angular/common/locales/en';
import localeEnExtra from '@angular/common/locales/extra/en';

import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FriendsComponent } from './components/friends/friends/friends.component';
import { MessengerComponent } from './components/messenger/messenger/messenger.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ConfirmAccountComponent } from './components/auth/register/confirm-account/confirm-account.component';
import { SendResetPasswordComponent } from './components/auth/send-reset-password/send-reset-password.component';
import { ResetPasswordComponent } from './components/auth/auth/reset-password/reset-password.component';
import { ProfileEditComponent } from './components/user/profile/profile-edit/profile-edit.component';
import { PostCardComponent } from './components/home/post-card/post-card.component';
import { PostComponent } from './components/home/post/post.component';
import { RequiredRolesDirective } from './directives/required-roles.directive';
import { ProfileResolver } from './resolvers/profile.resolver';
import { JwtModule } from '@auth0/angular-jwt';
import { PostsResolver } from './resolvers/posts.resolver';
import { PostResolver } from './resolvers/post.resolver';
import { CommentsComponent } from './components/home/post/comments/comments.component';
import { EditPostComponent } from './components/home/post/edit-post/edit-post.component';
import { MyPostsComponent } from './components/user/my-posts/my-posts.component';
import { FriendsResolver } from './resolvers/friends.resolver';
import { UsersComponent } from './components/friends/users/users.component';
import { UsersWrapperComponent } from './components/friends/users-wrapper/users-wrapper.component';
import { UsersResolver } from './resolvers/users.resolver';
import { StoriesComponent } from './components/friends/stories/stories.component';
import { StoriesResolver } from './resolvers/stories.resolver';
import { StoryComponent } from './components/friends/stories/story/story.component';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { ConversationsResolver } from './resolvers/conversations.resolver';
import { MessagesResolver } from './resolvers/messages.resolver';
import { MessagesThreadComponent } from './components/messenger/messages-thread/messages-thread.component';
import { MessageCardComponent } from './components/messenger/messages-thread/message-card/message-card.component';
import { ConversationCardComponent } from './components/messenger/messenger/conversation-card/conversation-card.component';
import { UserComponent } from './components/user/user/user.component';
import { UserResolver } from './resolvers/user.resolver';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { FriendButtonsComponent } from './components/helpers/friend-buttons/friend-buttons.component';
import { UsersAdminListComponent } from './components/admin/users-admin-list/users-admin-list.component';
import { RedirectAdminPanelComponent } from './components/admin/redirect-admin-panel/redirect-admin-panel.component';
import { LogsComponent } from './components/admin/logs-panel/logs.component';
import { LogsResolver } from './resolvers/logs.resolver';
import { SupportPanelComponent } from './components/support/panel/support-panel/support-panel.component';
import { ReportComponent } from './components/support/report/report.component';
import { ReportCardComponent } from './components/support/panel/support-panel/report-card/report-card.component';
import { CreateReportComponent } from './components/support/create-report/create-report.component';
import { CreateReportAnonymousComponent } from './components/support/create-report-anonymous/create-report-anonymous.component';
import { RepliesComponent } from './components/support/report/replies/replies.component';
import { ReportsFiltersComponent } from './components/support/panel/reports-filters/reports-filters.component';
import { ReportResolver } from './resolvers/report.resolver';
import { ReportsResolver } from './resolvers/reports.resolver';
import { CreateReplyComponent } from './components/support/report/create-reply/create-reply.component';
import { AttachmentsComponent } from './components/helpers/attachments/attachments.component';
import { AttachmentsListComponent } from './components/helpers/attachments/attachments-list/attachments-list.component';
import { HostListenerComponent } from './components/helpers/host-listener/host-listener.component';
import { GroupsComponent } from './components/groups/groups/groups.component';
import { GroupsResolver } from './resolvers/groups.resolver';
import { GroupCardComponent } from './components/groups/groups/group-card/group-card.component';
import { EditGroupComponent } from './components/groups/edit-group/edit-group.component';
import { ImageCardComponent } from './components/helpers/image-card/image-card.component';
import { GroupResolver } from './resolvers/group.resolver';
import { GroupComponent } from './components/groups/group/group.component';
import { GroupDetailsComponent } from './components/groups/group/group-details/group-details.component';
import { GroupDashboardComponent } from './components/groups/group/group-dashboard/group-dashboard.component';
import { UserGroupsComponent } from './components/groups/user-groups/user-groups.component';
import { UserGroupsResolver } from './resolvers/user-groups.resolver';
import { GroupMembersComponent } from './components/groups/group/group-members/group-members.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { NotificationsResolver } from './resolvers/notifications.resolver';
import { ChangeEmailComponent } from './components/user/profile/profile-edit/change-email/change-email.component';

export const tokenGetter = () => localStorage.getItem('token');

registerLocaleData(localeEn, 'en', localeEnExtra);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FriendsComponent,
    MessengerComponent,
    ProfileComponent,
    AuthComponent,
    LoginComponent,
    RegisterComponent,
    ConfirmAccountComponent,
    SendResetPasswordComponent,
    ResetPasswordComponent,
    ProfileEditComponent,
    PostCardComponent,
    PostComponent,
    CommentsComponent,
    EditPostComponent,
    MyPostsComponent,
    UsersComponent,
    UsersWrapperComponent,
    StoriesComponent,
    StoryComponent,
    MessagesThreadComponent,
    MessageCardComponent,
    ConversationCardComponent,
    UserComponent,
    AdminPanelComponent,
    FriendButtonsComponent,
    UsersAdminListComponent,
    RedirectAdminPanelComponent,
    LogsComponent,
    SupportPanelComponent,
    ReportComponent,
    ReportCardComponent,
    CreateReportComponent,
    CreateReportAnonymousComponent,
    RepliesComponent,
    ReportsFiltersComponent,
    CreateReplyComponent,
    AttachmentsComponent,
    AttachmentsListComponent,
    HostListenerComponent,
    GroupsComponent,
    GroupCardComponent,
    EditGroupComponent,
    GroupComponent,
    GroupDetailsComponent,
    GroupDashboardComponent,
    UserGroupsComponent,
    GroupMembersComponent,
    NotificationsComponent,
    ImageCardComponent,
    ChangeEmailComponent,

    RequiredRolesDirective,

    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,

    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatOptionModule,
    MatPaginatorModule,
    MatTabsModule,
    MatBadgeModule,
    MatCheckboxModule,

    InfiniteScrollModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    NotifierModule.withConfig(customNotifierOptions)
  ],
  providers: [
    PostResolver,
    PostsResolver,
    ProfileResolver,
    UsersResolver,
    FriendsResolver,
    StoriesResolver,
    ConversationsResolver,
    MessagesResolver,
    UserResolver,
    LogsResolver,
    ReportResolver,
    ReportsResolver,
    GroupResolver,
    GroupsResolver,
    UserGroupsResolver,
    NotificationsResolver,

    InterceptorProvider,
    { provide: LOCALE_ID, useValue: 'en-EN' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
