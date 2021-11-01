const colors = require('tailwindcss/colors')

module.exports = {
    purge: {
        enabled: true,
        content: [
            './**/*.html',
            './**/*.razor'
        ],
    },
    darkMode: false, // or 'media' or 'class'
    theme: {
        extend: {
            colors: {
                primary: colors.rose,
                secondary: colors.yellow,
            },
            typography: (theme) => ({
                DEFAULT: {
                    css: {
                        maxWidth: 'none',
                        pre: {
                            backgroundColor: theme('colors.gray.100'),
                            color: theme('colors.black'),
                            borderRadius: 0,
                            borderColor: theme('colors.gray.300'),
                            borderWidth: '1px',
                        },
                        img: {
                            marginTop: '.5em',
                            marginBottom: '.5em',
                            display: 'inline-block',
                        }
                    },
                },
            }),
        },
    },
    variants: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/typography'),
    ],
}
