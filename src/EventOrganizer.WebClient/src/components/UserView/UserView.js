import { Card, Stack, Typography } from '@mui/material'
import BackgroundLetterAvatar from './BackgroundLetterAvatar'

export default function UserView({ user }) {

    const getUserName = () => `${user.firstName} ${user.lastName}`;

    return user && (
        <Card sx={{ p: 0.5, display: 'flex', width: '250px' }}>
                <BackgroundLetterAvatar userName={getUserName()} />
                <Stack lineHeight={1} margin='auto 5px'>
                    <Typography fontWeight={400} fontSize='0.9em'> {getUserName()}</Typography>
                    <Typography fontWeight={100} fontSize='0.8em'> {user.email}</Typography>
                </Stack>
        </Card>
    )
}