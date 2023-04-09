import React, { useEffect } from 'react'
import authService from '../../services/authService'
import { useHistory } from 'react-router-dom'

function SignoutOidc() {
  const history = useHistory()
  useEffect(() => {
    async function signoutAsync() {
      await authService.signoutRedirectCallback()
      history.push('/')
    }
    signoutAsync()
  }, [history])

  return (
    <div>
      Redirecting...
    </div>
  )
}

export default SignoutOidc
