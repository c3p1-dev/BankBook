CXX=		c++
CXXFLAGS=	-O2 -Wall -std=c++20 -Iinclude -I/usr/local/include -MMD -MP
LDFLAGS=	-L/usr/local/lib
LIBS=		-lsqlite3

OUT=		bankbook
SRCDIR=		src
OBJDIR=		build

SRCS!=		ls ${SRCDIR}/*.cpp
OBJS=		${SRCS:T:R:@f@${OBJDIR}/${f}.o@}

all: ${OUT}

${OUT}: ${OBJS}
	${CXX} ${CXXFLAGS} ${LDFLAGS} -o ${.TARGET} ${OBJS} ${LIBS}

# Une règle par source : build/foo.o <- src/foo.cpp
.for f in ${SRCS}
${OBJDIR}/${f:T:R}.o: ${f}
	@mkdir -p ${.TARGET:H}
	${CXX} ${CXXFLAGS} -c ${f} -o ${.TARGET}
.endfor

# Dépendances sur les en-têtes (fichiers .d générés par -MMD)
.for o in ${OBJS}
.sinclude "${o:R}.d"
.endfor

clean:
	rm -rf ${OBJDIR} ${OUT}

.PHONY: all clean