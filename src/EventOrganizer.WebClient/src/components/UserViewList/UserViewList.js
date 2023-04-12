import { Stack } from '@mui/material'
import UserView from 'components/UserView/UserView'

export default function UserViewList ({ users }) {

    return users && (
        <Stack direction="column" spacing={1} sx={{ flexWrap: 'wrap' }}>
            {users.map(user => (
                <UserView key={user.id} user={user} />
            ))}
        </Stack>
    )
}