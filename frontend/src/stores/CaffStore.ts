import { doc, getDoc } from "firebase/firestore";
import { makeAutoObservable } from "mobx";
import { makePersistable, PersistStoreMap } from "mobx-persist-store";
import { db } from "../firebase";
import { caffService } from "../services/caffService";
import { Caff, caffMock, caffMock2 } from "../types/Caff";
import { toJS } from "mobx";
import { extendObservable } from "mobx";

export default class CaffStore {
  caffs: Caff[] = [];

  constructor() {
    makeAutoObservable(this, {}, { autoBind: true });

    const persist = () => {
      makePersistable(this, {
        name: "caffStore",
        properties: ["caffs"],
        storage: window.localStorage,
      });
    };

    const persistedStore = Array.from(PersistStoreMap.values()).find((el) =>
      el.storageName.includes("caffStore")
    );
    if (persistedStore) {
      persistedStore.stopPersisting();
    }
    persist();
  }

  async getUserName(caffIndex: number) {
    const docRef = doc(db, "users", this.caffs[caffIndex].uploaderId);
    const docSnap = await getDoc(docRef);
    if (docSnap.exists()) {
      if (docSnap.data()?.username) return docSnap.data()?.username;
    }
  }

  async getCaffs() {
    const result = toJS(await caffService.getCaffs());

    if (result) {
      result.map(async (caff, index) => {
        if (!caff.uploader)
          result[index].uploader = await this.getUserName(index);
      });
      this.caffs = result;
    } else {
      this.caffs = [];
    }
  }

  getCaffById(id: number) {
    return this.caffs.find((x) => x.id === id);
  }
}
