import React, { useEffect } from 'react'
import authService from 'services/authService'
import { useHistory } from 'react-router-dom'

function SigninOidc() {
  const history = useHistory()
  useEffect(() => {
    async function signinAsync() {
      await authService.signinRedirectCallback()
      history.push('/')
    }
    signinAsync()
  }, [history])

  return (
    <div>
      Redirecting...
    </div>
  )
}

export default SigninOidc
