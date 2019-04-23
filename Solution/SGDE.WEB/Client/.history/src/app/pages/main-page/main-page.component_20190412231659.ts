import {Component, HostListener, OnInit, ViewEncapsulation} from '@angular/core';
import {ResizeService} from '../../resize/resize.service';
import {routerTransition} from '../../utils/page.animation';
import {Router} from '@angular/router';

import {StorageService} from '../../shared/services/storage.service';

/**
 * This page wraps all other pages in application, it contains header, side menu and router outlet for child pages
 */
@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
  animations: [routerTransition],
  encapsulation: ViewEncapsulation.None
})

export class MainPageComponent implements OnInit {
  // Model for side menu
  menuModel = [
    {
      title: 'Dashboard',
      routerUrl: '/main/dashboard',
      iconClass: 'material-icons',
      iconCode: 'dashboard',
    },
    {
      title: 'Users',
      routerUrl: '/main/users',
      iconClass: 'material-icons',
      iconCode: 'people',
    },
    {
      title: 'Professions',
      routerUrl: '/main/professions',
      iconClass: 'material-icons',
      iconCode: 'build',
    }
    // {
    //   title: 'User pages',
    //   iconClass: 'material-icons',
    //   iconCode: 'person',
    //   children: [
    //     {
    //       title: 'Login',
    //       routerUrl: '/login'
    //     },
    //     {
    //       title: 'Sing up',
    //       routerUrl: '/register'
    //     },
    //     {
    //       title: 'Profile',
    //       routerUrl: '/main/profile'
    //     },
    //     {
    //       title: 'Coming soon',
    //       routerUrl: '/coming-soon'
    //     },
    //     {
    //       title: 'Maintenance',
    //       routerUrl: '/maintenance'
    //     },
    //     {
    //       title: 'Not found',
    //       routerUrl: '/404'
    //     }
    //   ]
    // }
  ];
  // Side menu options
  isSmallMenuMode = false;
  isMenuCollapsed = false;
  isMenuClosed = this.isSmallWidth();
  isOverlayMenuMode = this.isSmallWidth();
  // Side menu animation value. Is used for delay to render content after side panel changes
  sideNavTransitionDuration = 300;
  // Open/close options window
  isOptionsClosed = true;
  // Box layout option
  isBoxedLayout = false;
  // Fixed header option
  isFixedHeader = true;
  currentUser: string;

  constructor(
    private resizeService: ResizeService,
    private router: Router,
    private storageService: StorageService) {
    this.onResize();
  }

  /**
   * Window resize listener
   */
  @HostListener('window:resize', ['$event'])
  onResize() {
    this.resizeService.resizeInformer$.next();
    if (this.isSmallWidth()) {
      this.isOverlayMenuMode = true;
      this.isMenuClosed = true;
      setTimeout(() => this.resizeService.resizeInformer$.next(), this.sideNavTransitionDuration + 700);
    }
  }

  /**
   * Call resize service after side panel mode changes
   */
  onSideNavModeChange() {
    setTimeout(() => {
      this.resizeService.resizeInformer$.next();
    }, this.sideNavTransitionDuration)
  }

  ngOnInit(): void {
    this.currentUser = this.storageService.getCurrentUser().name + ' ' + this.storageService.getCurrentUser().surname;
  }

  /**
   * Call resize service after box mode changes
   */
  toggleBoxed() {
    this.isBoxedLayout = !this.isBoxedLayout;
    setTimeout(() => {
      this.resizeService.resizeInformer$.next()
    }, 0);
  }

  toggleCompactMenu() {
    this.isSmallMenuMode = !this.isSmallMenuMode;
    setTimeout(() => {
      this.resizeService.resizeInformer$.next()
    }, this.sideNavTransitionDuration);
  }

  /**
   * Call resize service after side panel mode changes
   */
  toggleOverlayMode() {
    this.isOverlayMenuMode = !this.isOverlayMenuMode;
    setTimeout(() => {
      this.resizeService.resizeInformer$.next()
    }, this.sideNavTransitionDuration);
  }

  /**
   * Changes header mode
   */
  toggleFixedHeader() {
    this.isFixedHeader = !this.isFixedHeader;
  }

  /**
   * Return url as state, that will trigger animation when url changes
   * @param outlet
   * @returns {string}
   */
  getState(outlet) {
    return this.router.url;
  }

  private isSmallWidth() {
    return window.innerWidth < 700;
  }

  public logout() {
    localStorage.removeItem('isLoggedin');
    this.storageService.logout();
  }
}
