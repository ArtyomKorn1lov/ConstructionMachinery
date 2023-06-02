import { EventEmitter } from '@angular/core';

export interface CommonChild {
    executeAction(): any;
}
let subscription: { unsubscribe: () => void; };
export function eventSubscriber(action: EventEmitter<any>, handler: () => void, off: boolean = false) {
    if (off && subscription) {
        subscription.unsubscribe();
    } else {
        subscription = action.subscribe(() => handler());
    }
}
