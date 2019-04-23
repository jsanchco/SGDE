import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Profession } from '../models/profession.model';
import { StorageService } from './storage.service';

@Injectable({ providedIn: 'root' })

export class ProfessionsService {

  constructor(
    private http: HttpClient,
    private storageService: StorageService) { }

  getAll() {
    const url = this.storageService.getBaseApiUrl() + 'professions';
    return this.http.get<Profession[]>(url);
  }
}
