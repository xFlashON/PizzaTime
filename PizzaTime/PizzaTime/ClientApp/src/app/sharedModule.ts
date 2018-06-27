import { NgModule, ModuleWithProviders } from '@angular/core';
import { OrderServise } from './services/orderServise';
import { ProtectionServise } from './services/protectionServise';

@NgModule({
})
export class SharedModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                OrderServise,
                ProtectionServise]
        }
    }
}