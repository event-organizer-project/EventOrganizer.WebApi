import { Box } from '@mui/material'

export default function EventTagList ({ tags }) {

    return tags && (
        <Box sx={{ display: 'flex' }}>
            {tags.map(tag => (
                <Box key={tag} sx={{
                    borderRadius: 1,
                    backgroundColor: '#80808066',
                    mx: 0.5,
                    px: 0.5
                  }}>
                    #{ tag }
                </Box>
            ))}
        </Box>
    )
}