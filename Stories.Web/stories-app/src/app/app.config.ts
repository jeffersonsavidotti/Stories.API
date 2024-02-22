import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { AppRoutingModule } from './app.routes';
import { BrowserModule } from '@angular/platform-browser';


export const appConfig: ApplicationConfig = {
  providers: [importProvidersFrom(HttpClientModule), HttpClientModule, BrowserModule, AppRoutingModule, provideRouter(routes), provideClientHydration(), provideAnimationsAsync(), provideHttpClient(), provideAnimationsAsync(), provideHttpClient(withFetch()), provideAnimationsAsync(), provideAnimationsAsync()]
};
