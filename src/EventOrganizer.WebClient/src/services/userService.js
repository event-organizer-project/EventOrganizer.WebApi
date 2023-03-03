import { UserManager } from 'oidc-client';
import { storeUserError, storeUser, getUserSlice } from '../store/authSlice'
  
const config = {
  authority: "https://localhost:7288",
  client_id: "eventorganizer",
  redirect_uri: "https://localhost:3000/signin-oidc",
  response_type: "id_token token",
  scope: "openid profile eventorganizerapi",
  post_logout_redirect_uri: "https://localhost:3000/signout-oidc",
};

const userManager = new UserManager(config)

export async function loadUserFromStorage(store) {
  try {
    let user = await userManager.getUser()
    if (!user) { return store.dispatch(storeUserError()) }
      store.dispatch(storeUser(getUserSlice(user)))
  } catch (e) {
    console.error("loadUserFromStorage: ", e)
    store.dispatch(storeUserError())
  }
}

export function signinRedirect() {
  return userManager.signinRedirect()
}

export function signinRedirectCallback() {
  return userManager.signinRedirectCallback()
}

export function signoutRedirect() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirect()
}

export function signoutRedirectCallback() {
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirectCallback()
}

export default userManager