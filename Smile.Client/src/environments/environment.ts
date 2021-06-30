import { animate, style, transition, trigger } from '@angular/animations';
import { NotifierOptions } from 'angular-notifier';

export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api/',
  signalRUrl: 'http://localhost:5000/api/hub/'
};

export const constants = {
  minUsernameLength: 5,
  maxUsernameLength: 24,
  minPasswordLength: 5,
  maxPasswordLength: 30,
  titleLength: 150,
  contentLength: 1500,
  commentLength: 500,
  messageLength: 250,

  maxFilesCount: 5,
  maxFileSize: 3,

  groupCodeLength: 16,

  localHostLength: 'http://localhost'.length
};

export const colors = {
  primaryColor: '#212121',
  secondaryColor: '#ffffff',
  thirdColor: '#343a40',
  destructiveColor: '#f03448',
  greenColor: '#56a86c',
  blueColor: '#bac7ff'
};

export const pageSize = 10;
export const logsPageSize = 50;

export const roles = {
  adminRole: 'Admin',
  headAdminRole: 'HeadAdmin',
  adminRoles: ['Admin', 'HeadAdmin']
};

export const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
      distance: 12
    },
    vertical: {
      position: 'bottom',
      distance: 12,
      gap: 10
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 5000,
    onClick: false,
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 1
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};

export const blurToggleAnimation = [
  trigger('blurToggle', [
    transition(':enter', [
      style({ opacity: 0 }),
      animate('.3s ease-out', style({ opacity: 1 }))
    ]),
    transition(':leave', [
      animate('.3s ease-out', style({ opacity: 0 }))
    ])
  ])
];
