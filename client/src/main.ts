import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

// code for boostrapping our appmodule whioch boostraps our components
platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
