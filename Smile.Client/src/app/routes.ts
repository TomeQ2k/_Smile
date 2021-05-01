import { Routes } from '@angular/router';
import { roles } from 'src/environments/environment';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { LogsComponent } from './components/admin/logs-panel/logs.component';
import { UsersAdminListComponent } from './components/admin/users-admin-list/users-admin-list.component';
import { AuthComponent } from './components/auth/auth/auth.component';
import { ResetPasswordComponent } from './components/auth/auth/reset-password/reset-password.component';
import { ConfirmAccountComponent } from './components/auth/register/confirm-account/confirm-account.component';
import { UsersWrapperComponent } from './components/friends/users-wrapper/users-wrapper.component';
import { EditGroupComponent } from './components/groups/edit-group/edit-group.component';
import { GroupComponent } from './components/groups/group/group.component';
import { GroupsComponent } from './components/groups/groups/groups.component';
import { HomeComponent } from './components/home/home.component';
import { EditPostComponent } from './components/home/post/edit-post/edit-post.component';
import { PostComponent } from './components/home/post/post.component';
import { MessagesThreadComponent } from './components/messenger/messages-thread/messages-thread.component';
import { MessengerComponent } from './components/messenger/messenger/messenger.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { CreateReportAnonymousComponent } from './components/support/create-report-anonymous/create-report-anonymous.component';
import { CreateReportComponent } from './components/support/create-report/create-report.component';
import { SupportPanelComponent } from './components/support/panel/support-panel/support-panel.component';
import { ReportComponent } from './components/support/report/report.component';
import { ChangeEmailComponent } from './components/user/profile/profile-edit/change-email/change-email.component';
import { ProfileComponent } from './components/user/profile/profile.component';
import { UserComponent } from './components/user/user/user.component';
import { AnonymousGuard } from './guards/anonymous.guard';
import { AuthGuard } from './guards/auth.guard';
import { ConversationsResolver } from './resolvers/conversations.resolver';
import { FriendsResolver } from './resolvers/friends.resolver';
import { GroupResolver } from './resolvers/group.resolver';
import { GroupsResolver } from './resolvers/groups.resolver';
import { LogsResolver } from './resolvers/logs.resolver';
import { MessagesResolver } from './resolvers/messages.resolver';
import { NotificationsResolver } from './resolvers/notifications.resolver';
import { PostResolver } from './resolvers/post.resolver';
import { PostsResolver } from './resolvers/posts.resolver';
import { ProfileResolver } from './resolvers/profile.resolver';
import { ReportResolver } from './resolvers/report.resolver';
import { ReportsResolver } from './resolvers/reports.resolver';
import { StoriesResolver } from './resolvers/stories.resolver';
import { UserGroupsResolver } from './resolvers/user-groups.resolver';
import { UserResolver } from './resolvers/user.resolver';
import { UsersResolver } from './resolvers/users.resolver';

export const routes: Routes = [
  { path: '', component: HomeComponent, resolve: { postsResponse: PostsResolver } },
  { path: 'posts/:postId', component: PostComponent, resolve: { postResponse: PostResolver } },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AnonymousGuard],
    children: [
      { path: 'auth', component: AuthComponent },
      { path: 'account/confirm', component: ConfirmAccountComponent },
      { path: 'account/resetPassword', component: ResetPasswordComponent },
      { path: 'report/createAnonymous', component: CreateReportAnonymousComponent },
    ]
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'profile', component: ProfileComponent,
        resolve: { profileResponse: ProfileResolver, postsResponse: PostsResolver, userGroupsResponse: UserGroupsResolver },
        data: { isProfile: true }
      },
      { path: 'profile/changeEmail', component: ChangeEmailComponent },
      { path: 'post/create', component: EditPostComponent },
      { path: 'post/edit/:postId', component: EditPostComponent, resolve: { postResponse: PostResolver } },
      {
        path: 'friends', component: UsersWrapperComponent, resolve:
          { usersResponse: UsersResolver, friendsResponse: FriendsResolver, storiesResponse: StoriesResolver }
      },
      { path: 'users/:userId', component: UserComponent, resolve: { userResponse: UserResolver } },
      { path: 'messenger', component: MessengerComponent, resolve: { conversationsResponse: ConversationsResolver } },
      { path: 'messenger/:recipientId', component: MessagesThreadComponent, resolve: { messagesResponse: MessagesResolver } },
      { path: 'report/create', component: CreateReportComponent },
      { path: 'support', component: SupportPanelComponent, resolve: { reportsResponse: ReportsResolver } },
      { path: 'support/:reportId', component: ReportComponent, resolve: { reportResponse: ReportResolver } },
      { path: 'groups', component: GroupsComponent, resolve: { groupsResponse: GroupsResolver } },
      { path: 'group/:groupId', component: GroupComponent, resolve: { groupResponse: GroupResolver } },
      { path: 'groups/create', component: EditGroupComponent },
      { path: 'notifications', component: NotificationsComponent, resolve: { notificationsResponse: NotificationsResolver } },
      { path: 'admin/panel', component: AdminPanelComponent, data: { roles: roles.adminRoles } },
      {
        path: 'admin/panel/users', component: UsersAdminListComponent, data: { roles: roles.adminRoles },
        resolve: { usersResponse: UsersResolver }
      },
      {
        path: 'admin/panel/logs', component: LogsComponent, data: { roles: roles.adminRoles },
        resolve: { logsResponse: LogsResolver }
      },
      {
        path: 'admin/support', component: SupportPanelComponent, data: { roles: roles.adminRoles, isAdmin: true },
        resolve: { reportsResponse: ReportsResolver }
      }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
